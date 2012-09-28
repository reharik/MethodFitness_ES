using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using MethodFitness.Core.Domain;
using MethodFitness.Core.Enumerations;
using MethodFitness.Web.Config;
using MethodFitness.Web.Controllers;

namespace MethodFitness.Web.Areas.Billing.Controllers
{
    public class PayTrainerController:MFController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public PayTrainerController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public CustomJsonResult PayTrainer(PayTrainerViewModel input)
        {
            Notification notification;
            var trainer = _repository.Find<Trainer>(input.EntityId);
            var trainerPayment = trainer.PayTrainer(input.eligableRows, input.paymentAmount);
            if(trainerPayment==null)
            {
                notification = new Notification {Success = false, Message = WebLocalizationKeys.YOU_MUST_SELECT_AT_ONE_SESSION.ToString()};
                return new CustomJsonResult { Data = notification };
            }
            var crudManager = _saveEntityService.ProcessSave(trainer);
            notification = crudManager.Finish();
            notification.Variable = UrlContext.GetUrlForAction<PayTrainerController>(x => x.TrainerReceipt(null),AreaName.Billing)+"/"+trainerPayment.EntityId+"?ParentId="+trainer.EntityId;
            return new CustomJsonResult { Data = notification };
        }

        public ActionResult TrainerReceipt(ViewModel input)
        {
            var trainer = _repository.Find<Trainer>(input.ParentId);
            var payment = trainer.TrainerPayments.FirstOrDefault(x => x.EntityId == input.EntityId);
            var model = new TrainerReceiptViewModel
                                              {
                                                  Trainer = trainer,
                                                  TrainerPayment = payment
                                              };
            return View(model);
        }
    }

    public class TrainerReceiptViewModel : ViewModel
    {
        public User Trainer { get; set; }
        public TrainerPayment TrainerPayment { get; set; }
    }
}