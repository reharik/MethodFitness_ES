﻿using System;
using System.Linq;
using System.Web.Mvc;
using MethodFitness.Core;
using MethodFitness.Core.Domain;
using MethodFitness.Core.Domain.Tools;
using MethodFitness.Core.Enumerations;
using MethodFitness.Core.Html;
using MethodFitness.Core.Rules;
using MethodFitness.Core.Services;
using MethodFitness.Web.Areas.Billing.Controllers;
using MethodFitness.Web.Areas.Portfolio.Models.BulkAction;
using MethodFitness.Web.Areas.Schedule.Controllers;
using StructureMap;

namespace MethodFitness.Web.Controllers
{
    public class ClientController : MFController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISessionContext _sessionContext;

        public ClientController(IRepository repository,
            ISaveEntityService saveEntityService, ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            Client client;
            if (input.EntityId > 0)
            {
                client = _repository.Find<Client>(input.EntityId);
                client.SessionRates.FullHour = client.SessionRates.FullHour > 0 ? client.SessionRates.FullHour : client.SessionRates.ResetFullHourRate();
                client.SessionRates.HalfHour = client.SessionRates.HalfHour > 0 ? client.SessionRates.HalfHour : client.SessionRates.ResetHalfHourRate();
                client.SessionRates.FullHourTenPack = client.SessionRates.FullHourTenPack > 0 ? client.SessionRates.FullHourTenPack : client.SessionRates.ResetFullHourTenPackRate();
                client.SessionRates.HalfHourTenPack = client.SessionRates.HalfHourTenPack > 0 ? client.SessionRates.HalfHourTenPack : client.SessionRates.ResetHalfHourTenPackRate();
                client.SessionRates.Pair = client.SessionRates.Pair > 0 ? client.SessionRates.Pair : client.SessionRates.ResetPairRate();
                client.SessionRates.PairTenPack = client.SessionRates.PairTenPack > 0 ? client.SessionRates.PairTenPack : client.SessionRates.ResetPairTenPackRate();
            }
            else
            {
                client = new Client { StartDate = DateTime.Now, SessionRates = new SessionRates(true) };
            }
            //hijacking sessionratesdto since I need exact same object just different name
            var clientSessionsDto = new SessionRatesDto
            {
                FullHour = client.Sessions.Any(x => x.AppointmentType == AppointmentType.Hour.ToString() && x.InArrears)
                    ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.Hour.ToString() && x.InArrears)
                    : client.Sessions.Count(x => x.AppointmentType == AppointmentType.Hour.ToString()&&!x.SessionUsed),
                HalfHour = client.Sessions.Any(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && x.InArrears)
                    ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && x.InArrears)
                    : client.Sessions.Count(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && !x.SessionUsed),
                Pair = client.Sessions.Any(x => x.AppointmentType == AppointmentType.Pair.ToString() && x.InArrears)
                    ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.Pair.ToString() && x.InArrears)
                    : client.Sessions.Count(x => x.AppointmentType == AppointmentType.Pair.ToString() && !x.SessionUsed),
            };
            var model = new ClientViewModel
            {
                Item = client,
                Title = WebLocalizationKeys.CLIENT_INFORMATION.ToString(),
                DeleteUrl = UrlContext.GetUrlForAction<ClientController>(x=>x.Delete(null)),
                PaymentListUrl = UrlContext.GetUrlForAction<PaymentListController>(x=>x.ItemList(null),AreaName.Billing)+"?ParentId="+client.EntityId,
                SessionsAvailable = clientSessionsDto
            };
            return View(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var client = _repository.Find<Client>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteClientRules");
            var validationManager = rulesEngineBase.ExecuteRules(client);
            if (validationManager.GetLastValidationReport().Success)
            {
                _repository.Delete(client);
            }
            var notification = validationManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteClientRules");
            IValidationManager<Client> validationManager = new ValidationManager<Client>(_repository);
            input.EntityIds.Each(x =>
            {
                var client = _repository.Find<Client>(x);
                validationManager = rulesEngineBase.ExecuteRules(client, validationManager);
                var report = validationManager.GetLastValidationReport();
                if (report.Success)
                {
                    report.SuccessAction = a => _repository.Delete(a);
                }
            });
            var notification = validationManager.FinishWithAction();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(ClientViewModel input)
        {
            Client client;
            client = input.EntityId > 0 ? _repository.Find<Client>(input.EntityId) : new Client();
            client = mapToDomain(input, client);
            associateWithUser(client);
//            if (input.DeleteImage)
//            {
////                _uploadedFileHandlerService.DeleteFile(client.ImageUrl);
//                client.ImageUrl = string.Empty;
//            }
//            
//            var file = _uploadedFileHandlerService.RetrieveUploadedFile();
////            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Clients";
//            client.ImageUrl = _uploadedFileHandlerService.GetUrlForFile(file, client.FirstName + "_" + client.LastName);
            var crudManager = _saveEntityService.ProcessSave(client);

//            _uploadedFileHandlerService.SaveUploadedFile(file, client.FirstName + "_" + client.LastName);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private void associateWithUser(Client client)
        {
            var userEntityId = _sessionContext.GetUserEntityId();
            var trainer = _repository.Find<User>(userEntityId);
            if(trainer is Trainer)
            {
                ((Trainer)trainer).AddClient(client, ((Trainer)trainer).ClientRateDefault);
            }
            _saveEntityService.ProcessSave(trainer);
        }

        private Client mapToDomain(ClientViewModel model, Client client)
        {
            var clientModel = model.Item;
            client.Address1 = clientModel.Address1;
            client.Address2 = clientModel.Address2;
            client.FirstName = clientModel.FirstName;
            client.LastName = clientModel.LastName;
            client.Email = clientModel.Email;
            client.MobilePhone = clientModel.MobilePhone;
            client.City = clientModel.City;
            client.State = clientModel.State;
            client.ZipCode = clientModel.ZipCode;
            client.Notes = clientModel.Notes;
            client.Status = clientModel.Status;
            client.SourceOther = clientModel.SourceOther;
            client.Source = clientModel.Source;
            client.BirthDate = clientModel.BirthDate;
            client.StartDate = clientModel.StartDate;
            if (client.SessionRates == null) {client.SessionRates = new SessionRates(true);}
            else if(clientModel.SessionRates!=null)
            {
                client.SessionRates.FullHour = clientModel.SessionRates.FullHour;
                client.SessionRates.HalfHour = clientModel.SessionRates.HalfHour;
                client.SessionRates.FullHourTenPack = clientModel.SessionRates.FullHourTenPack;
                client.SessionRates.HalfHourTenPack = clientModel.SessionRates.HalfHourTenPack;
                client.SessionRates.Pair = clientModel.SessionRates.Pair;
                client.SessionRates.PairTenPack = clientModel.SessionRates.PairTenPack;
            }
            return client;
        }
    }

    public class ClientViewModel:ViewModel
    {
        public Client Item { get; set; }
        public string DeleteUrl { get; set; }
        public string PaymentListUrl { get; set; }

        public SessionRatesDto SessionsAvailable { get; set; }
    }
}