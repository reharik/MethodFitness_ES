﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.ValidationServices;
using MF.Core.Domain;
using MF.Web.Config;
using MF.Web.Controllers;
using MF.Web;

namespace MF.Web.Areas.Billing.Controllers
{
    public class BaseSessionRateController : MFController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public BaseSessionRateController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate_Template(ViewModel input)
        {
            return View("AddUpdate", new BaseSessionRateViewModel());
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var baseSessionRate = _repository.Query<BaseSessionRate>().FirstOrDefault();
            var model = new BaseSessionRateViewModel
                {
                    FullHour = baseSessionRate.FullHour,
                    HalfHour = baseSessionRate.HalfHour,
                    FullHourTenPack = baseSessionRate.FullHourTenPack,
                    HalfHourTenPack = baseSessionRate.HalfHourTenPack,
                    Pair = baseSessionRate.Pair,
                    PairTenPack = baseSessionRate.PairTenPack,
                    _Title = WebLocalizationKeys.BASE_RATES.ToString(),
                    _saveUrl = UrlContext.GetUrlForAction<BaseSessionRateController>(x => x.Save(null))
                };
            return new CustomJsonResult(model);
        }

        public ActionResult Save(BaseSessionRateViewModel input)
        {
            var orig = _repository.Query<BaseSessionRate>().FirstOrDefault();
            orig.FullHour = input.FullHour;
            orig.HalfHour = input.HalfHour;
            orig.FullHourTenPack = input.FullHourTenPack;
            orig.HalfHourTenPack = input.HalfHourTenPack;
            orig.Pair = input.Pair;
            orig.PairTenPack = input.PairTenPack;
            var crudManager = _saveEntityService.ProcessSave(orig);
            var notification = crudManager.Finish();
            return new CustomJsonResult(notification){ ContentType = "text/plain" };
        }
    }

    public class BaseSessionRateViewModel:ViewModel
    {
        [Required]
        public virtual double FullHour { get; set; }
        [Required]
        public virtual double HalfHour { get; set; }
        [Required]
        public virtual double FullHourTenPack { get; set; }
        [Required]
        public virtual double HalfHourTenPack { get; set; }
        [Required]
        public virtual double Pair { get; set; }
        [Required]
        public virtual double PairTenPack { get; set; }
    }
}