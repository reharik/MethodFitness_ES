﻿namespace MethodFitness.Web.Areas.Reporting.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;
    using CC.Core.DomainTools;
    using CC.Core.Services;

    using Castle.Components.Validator;

    using MethodFitness.Core.Domain;
    using MethodFitness.Web.Config;
    using MethodFitness.Web.Controllers;

    public class DailyPaymentsController : AdminController
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public DailyPaymentsController(IRepository repository, ISelectListItemService selectListItemService)
        {
            this._repository = repository;
            this._selectListItemService = selectListItemService;
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new DailyPaymentViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            var trainers = this._repository.Query<User>(x => x.UserRoles.Any(y => y.Name == "Trainer"));
            var clients = this._repository.Query<Client>();

            var model = new DailyPaymentViewModel
                            {
                                _TrainerList = this._selectListItemService.CreateList(trainers, x => x.FullNameFNF, x => x.EntityId, true),
                                _ClientList = this._selectListItemService.CreateList<Client>(x => x.FullNameFNF, x => x.EntityId, true),
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now,
                                _Title = WebLocalizationKeys.DAILY_PAYMENTS.ToString(),
                                ReportUrl = "/Areas/Reporting/ReportViewer/DailyPayments.aspx"
                            };
            return new CustomJsonResult(model);
        }
    }

    public class DailyPaymentViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _TrainerList { get; set; }
        public int Trainer { get; set; }
        public IEnumerable<SelectListItem> _ClientList { get; set; }
        public int Client { get; set; }
        [ValidateNonEmpty]
        public DateTime StartDate { get; set; }
        [ValidateNonEmpty]
        public DateTime EndDate { get; set; }
        public string ReportUrl { get; set; }
    }
}
