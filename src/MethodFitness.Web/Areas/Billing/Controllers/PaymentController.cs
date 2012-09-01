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
        private readonly IClientPaymentToSessions _clientPaymentToSessions;

        public PaymentController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISessionContext sessionContext,
            IClientPaymentToSessions clientPaymentToSessions)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
            _clientPaymentToSessions = clientPaymentToSessions;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var payment = input.EntityId > 0 ? _repository.Find<Payment>(input.EntityId) : new Payment();
            var client = input.ParentId > 0 ? _repository.Find<Client>(input.ParentId) : payment.Client;
            var sessionRatesDto = new SessionRatesDto
                                      {
                                          FullHour = client.SessionRates.FullHour > 0 ? client.SessionRates.FullHour : client.SessionRates.ResetFullHourRate(),
                                          HalfHour = client.SessionRates.HalfHour > 0 ? client.SessionRates.HalfHour : client.SessionRates.ResetHalfHourRate(),
                                          FullHourTenPack = client.SessionRates.FullHourTenPack > 0 ? client.SessionRates.FullHourTenPack : client.SessionRates.ResetFullHourTenPackRate(),
                                          HalfHourTenPack = client.SessionRates.HalfHourTenPack > 0 ? client.SessionRates.HalfHourTenPack : client.SessionRates.ResetHalfHourTenPackRate(),
                                          Pair = client.SessionRates.Pair > 0 ? client.SessionRates.Pair : client.SessionRates.ResetPairRate(),
                                          PairTenPack = client.SessionRates.PairTenPack > 0 ? client.SessionRates.PairTenPack : client.SessionRates.ResetPairTenPackRate(),
                                      };
            //hijacking sessionratesdto since I need exact same object just different name
            var clientSessionsDto = new SessionRatesDto
                                        {
                                            FullHour = client.Sessions.Any(x => x.AppointmentType == AppointmentType.Hour.ToString() && x.InArrears)
                                                ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.Hour.ToString() && x.InArrears)
                                                : client.Sessions.Count(x => x.AppointmentType == AppointmentType.Hour.ToString() && !x.SessionUsed),
                                            HalfHour = client.Sessions.Any(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && x.InArrears)
                                                ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && x.InArrears)
                                                : client.Sessions.Count(x => x.AppointmentType == AppointmentType.HalfHour.ToString() && !x.SessionUsed),
                                            Pair = client.Sessions.Any(x => x.AppointmentType == AppointmentType.Pair.ToString() && x.InArrears)
                                                ? -client.Sessions.Count(x => x.AppointmentType == AppointmentType.Pair.ToString() && x.InArrears)
                                                : client.Sessions.Count(x => x.AppointmentType == AppointmentType.Pair.ToString() && !x.SessionUsed),
                                        };
            var model = new PaymentViewModel
            {
                Item = payment,
                SessionRateDto = sessionRatesDto,
                SessionsAvailable = clientSessionsDto,
                Title = WebLocalizationKeys.PAYMENT_INFORMATION.ToString(),
                DeleteUrl = UrlContext.GetUrlForAction<PaymentController>(x=>x.Delete(null)),
                ParentId = client.EntityId
            };
            return View(model);
        }

        public ActionResult Display(ViewModel input)
        {
            var payment =  _repository.Find<Payment>(input.EntityId);
            var model = new PaymentViewModel
            {
                Item = payment,
                Title = WebLocalizationKeys.PAYMENT_INFORMATION.ToString(),
            };
            return View(model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var payment = _repository.Find<Payment>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeletePaymentRules");
            var rulesResult = rulesEngineBase.ExecuteRules(payment);
            if (rulesResult.GetLastValidationReport().Success)
            {
                _repository.Delete(payment);
            }
            var notification = rulesResult.FinishWithAction();
            return Json(notification, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeletePaymentRules");
            IValidationManager<Payment> validationManager = new ValidationManager<Payment>(_repository);
            input.EntityIds.Each(x =>
            {
                var payment = _repository.Find<Payment>(input.EntityId);
                validationManager = rulesEngineBase.ExecuteRules(payment, validationManager);
                var report = validationManager.GetLastValidationReport();
                if (report.Success)
                {
                    report.SuccessAction = a => _repository.Delete(a);
                }
            });
            var notification = validationManager.FinishWithAction();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(PaymentViewModel input)
        {
            var client = _repository.Find<Client>(input.ParentId);
            Payment payment;
            if (input.EntityId > 0)
            {
                payment = client.Payments.FirstOrDefault(x=>x.EntityId== input.EntityId);
            }
            else
            {
                payment = new Payment{Client = client, PaymentBatchId = Guid.NewGuid()};
            }
            payment = mapToDomain(input, payment);
            client = _clientPaymentToSessions.Execute(client, payment);
            client.AddPayment(payment);
            var crudManager = _saveEntityService.ProcessSave(client);

            var notification = crudManager.Finish();
            return Json(notification, "text/plain");
        }


        private Payment mapToDomain(PaymentViewModel model, Payment payment)
        {
            var paymentModel = model.Item;
            payment.FullHourTenPacks = paymentModel.FullHourTenPacks;
            payment.FullHourTenPacksPrice = paymentModel.FullHourTenPacksPrice;
            payment.FullHours = paymentModel.FullHours;
            payment.FullHoursPrice = paymentModel.FullHoursPrice;
            payment.HalfHourTenPacks = paymentModel.HalfHourTenPacks;
            payment.HalfHourTenPacksPrice = paymentModel.HalfHourTenPacksPrice;
            payment.HalfHours = paymentModel.HalfHours;
            payment.HalfHoursPrice = paymentModel.HalfHoursPrice;
            payment.Pairs = paymentModel.Pairs;
            payment.PairsPrice = paymentModel.PairsPrice;
            payment.PairsTenPack = paymentModel.PairsTenPack;
            payment.PairsTenPackPrice = paymentModel.PairsTenPackPrice;
            payment.PaymentTotal= paymentModel.PaymentTotal;
            return payment;
        }
    }

    public class SessionRatesDto
    {
        public double FullHour { get; set; }

        public double HalfHour { get; set; }

        public double FullHourTenPack { get; set; }

        public double HalfHourTenPack { get; set; }

        public double Pair { get; set; }

        public double PairTenPack { get; set; }
    }

    public class PaymentViewModel:ViewModel
    {
        public Payment Item { get; set; }
        public string DeleteUrl { get; set; }
        public double Total { get; set; }
        public SessionRatesDto SessionRateDto { get; set; }
        public SessionRatesDto SessionsAvailable { get; set; }
    }
}