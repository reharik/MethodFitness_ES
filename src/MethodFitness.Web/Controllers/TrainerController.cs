﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using MethodFitness.Core;
using MethodFitness.Core.CoreViewModelAndDTOs;
using MethodFitness.Core.Domain;
using MethodFitness.Core.Domain.Tools;
using MethodFitness.Core.Enumerations;
using MethodFitness.Core.Html;
using MethodFitness.Core.Localization;
using MethodFitness.Core.Rules;
using MethodFitness.Core.Services;
using StructureMap;

namespace MethodFitness.Web.Controllers
{
    public class TrainerController : AdminController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly IFileHandlerService _uploadedFileHandlerService;
        private readonly ISessionContext _sessionContext;
        private readonly ISecurityDataService _securityDataService;

        public TrainerController(IRepository repository,
            ISaveEntityService saveEntityService,
            IFileHandlerService uploadedFileHandlerService,
            ISessionContext sessionContext,
            ISecurityDataService securityDataService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _uploadedFileHandlerService = uploadedFileHandlerService;
            _sessionContext = sessionContext;
            _securityDataService = securityDataService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var trainer = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
            var userRoles = _repository.FindAll<UserRole>();
            var availableUserRoles = userRoles.Select(x => new TokenInputDto { id = x.EntityId, name = x.Name});
            var selectedUserRoles = trainer.UserRoles.Any()
                ? trainer.UserRoles.Select(x => new TokenInputDto { id = x.EntityId, name = x.Name })
                : null;

            var model = new UserViewModel
            {
                User = trainer,
                AvailableItems = availableUserRoles,
                SelectedItems = selectedUserRoles,
                Title = WebLocalizationKeys.TRAINER_INFORMATION.ToString()
            };
            return PartialView("TrainerAddUpdate", model);
        }

        public ActionResult Display(ViewModel input)
        {
            var trainer = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
            {
                User = trainer,
                AddEditUrl = UrlContext.GetUrlForAction<TrainerController>(x => x.AddUpdate(null)) + "/" + trainer.EntityId,
                Title = WebLocalizationKeys.TRAINER_INFORMATION.ToString()
            };
            return PartialView("TrainerView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var trainer = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteTrainerRules");
            var rulesResult = rulesEngineBase.ExecuteRules(trainer);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.Delete(trainer);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User trainer;
            if (input.EntityId > 0)
            {
                trainer = _repository.Find<User>(input.EntityId);
            }
            else
            {
                trainer = new User();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                trainer.Company = company;
            }
            trainer = mapToDomain(input, trainer);
            handlePassword(input, trainer);
//            if (input.DeleteImage)
//            {
////                _uploadedFileHandlerService.DeleteFile(trainer.ImageUrl);
//                trainer.ImageUrl = string.Empty;
//            }
//            
//            var file = _uploadedFileHandlerService.RetrieveUploadedFile();
////            var serverDirectory = "/CustomerPhotos/" + _sessionContext.GetCompanyId() + "/Trainers";
//            trainer.ImageUrl = _uploadedFileHandlerService.GetUrlForFile(file, trainer.FirstName + "_" + trainer.LastName);
            var crudManager = _saveEntityService.ProcessSave(trainer);

//            _uploadedFileHandlerService.SaveUploadedFile(file, trainer.FirstName + "_" + trainer.LastName);
            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }

        private void handlePassword(UserViewModel input, User origional)
        {
            if (input.Password.IsNotEmpty())
            {
                origional.UserLoginInfo.Salt = _securityDataService.CreateSalt();
                origional.UserLoginInfo.Password = _securityDataService.CreatePasswordHash(input.Password,
                                                            origional.UserLoginInfo.Salt);
            }
        }

        private User mapToDomain(UserViewModel model, User trainer)
        {
            var trainerModel = model.User;
            trainer.Address1 = trainerModel.Address1;
            trainer.Address2 = trainerModel.Address2;
            trainer.FirstName = trainerModel.FirstName;
            trainer.LastName = trainerModel.LastName;
            trainer.Email = trainerModel.Email;
            trainer.PhoneMobile = trainerModel.PhoneMobile;
            trainer.City = trainerModel.City;
            trainer.State = trainerModel.State;
            trainer.ZipCode = trainerModel.ZipCode;
            trainer.Notes = trainerModel.Notes;
            trainer.Status = trainerModel.Status;
            trainer.BirthDate = trainerModel.BirthDate;
            
            trainer.UserLoginInfo = new UserLoginInfo()
            {
                LoginName = trainer.Email
            };
            return trainer;
        }
    }

    public class UserViewModel:ViewModel
    {
        public User User { get; set; }
        public IEnumerable<TokenInputDto> AvailableItems { get; set; }
        public IEnumerable<TokenInputDto> SelectedItems { get; set; }
        public bool DeleteImage { get; set; }
        [ValidateSameAs("Password")]
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}