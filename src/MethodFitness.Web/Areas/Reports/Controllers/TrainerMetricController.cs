﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Services;
using Castle.Components.Validator;
using MethodFitness.Core.Domain;
using MethodFitness.Core.Enumerations;
using MethodFitness.Core.Services;
using MethodFitness.Web.Config;
using MethodFitness.Web.Controllers;

namespace MethodFitness.Web.Areas.Reports.Controllers
{
    public class TrainerMetricController : AdminController
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public TrainerMetricController(IRepository repository, ISelectListItemService selectListItemService)
        {
            _repository = repository;
            _selectListItemService = selectListItemService;
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return View("Display", new TrainerMetricViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            var startDate = DateTime.Now;
            var model = new TrainerMetricViewModel
                            {
                                Date = startDate,
                                _Title = WebLocalizationKeys.DAILY_PAYMENTS.ToString(),
                                ReportUrl = "/Areas/Reports/ReportViewer/DailyPayments.aspx"
                            };
            return new CustomJsonResult(model);
        }
    }

    public class TrainerMetricViewModel : ViewModel
    {
        public DateTime Date { get; set; }
        public string ReportUrl { get; set; }
    }
}
