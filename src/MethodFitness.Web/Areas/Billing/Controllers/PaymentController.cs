﻿using System.Linq;
using System.Web.Mvc;
using MethodFitness.Core;
using MethodFitness.Core.Domain;
using MethodFitness.Core.Domain.Tools;
using MethodFitness.Core.Html;
using MethodFitness.Core.Rules;
using MethodFitness.Core.Services;
using MethodFitness.Web.Areas.Portfolio.Models.BulkAction;
using MethodFitness.Web.Controllers;
using NHibernate.Linq;
using StructureMap;

namespace MethodFitness.Web.Areas.Billing.Controllers
{
    public class PaymentController : MFController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISessionContext _sessionContext;

        public PaymentController(IRepository repository,
            ISaveEntityService saveEntityService, ISessionContext sessionContext)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
//            var client = _repository.QueryWithFetch<Client, SessionRates>(x => x.EntityId == input.ParentId, x => x.SessionRates).ToList().First();

            var clientx = _repository.Query<Client>(x => x.EntityId == input.ParentId);
            var client = _repository.Query<Client>(x => x.EntityId == input.ParentId).First();
            var payment = input.EntityId > 0 ? client.Payments.FirstOrDefault(x => x.EntityId == input.EntityId) : new Payment { Client = client};
            var model = new PaymentViewModel
            {
                Item = payment,
                Title = WebLocalizationKeys.CLIENT_INFORMATION.ToString(),
                DeleteUrl = UrlContext.GetUrlForAction<PaymentController>(x=>x.Delete(null))
            };
            return View(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var payment = _repository.Find<Payment>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeletePaymentRules");
            var rulesResult = rulesEngineBase.ExecuteRules(payment);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification,JsonRequestBehavior.AllowGet);
            }
            _repository.Delete(payment);
            _repository.UnitOfWork.Commit();
            return Json(new Notification{Success = true}, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Payment>(x);
                _repository.Delete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(PaymentViewModel input)
        {
            Payment payment;
            payment = input.EntityId > 0 ? _repository.Find<Payment>(input.EntityId) : new Payment();
            payment = mapToDomain(input, payment);
            var crudManager = _saveEntityService.ProcessSave(payment);

            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }


        private Payment mapToDomain(PaymentViewModel model, Payment payment)
        {
            var paymentModel = model.Item;
            return payment;
        }
    }

    public class PaymentViewModel:ViewModel
    {
        public Payment Item { get; set; }
        public string DeleteUrl { get; set; }
        public double Total { get; set; }
    }
}