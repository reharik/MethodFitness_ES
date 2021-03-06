﻿using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using MF.Core.Domain;
using MF.Core.Services;
using MF.Web.Areas.Schedule.Grids;
using MF.Web.Config;
using MF.Web;

namespace MF.Web.Controllers
{
    public class LocationListController : MFController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Location> _locationListGrid;
        private readonly ISessionContext _sessionContext;
        private readonly IRepository _repository;

        public LocationListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Location> locationListGrid,
            ISessionContext sessionContext,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _locationListGrid = locationListGrid;
            _sessionContext = sessionContext;
            _repository = repository;
        }

        public JsonResult ItemList(ViewModel input)
        {
            var user = _sessionContext.GetCurrentUser();
            var url = UrlContext.GetUrlForAction<LocationListController>(x => x.Locations(null));
            var model = new ListViewModel()
            {
                addUpdateUrl = UrlContext.GetUrlForAction<LocationController>(x => x.AddUpdate(null)),
                deleteMultipleUrl = UrlContext.GetUrlForAction<LocationController>(x => x.DeleteMultiple(null)),
                // PaymentUrl = UrlContext.GetUrlForAction<PaymentListController>(x => x.ItemList(null),AreaName.Billing),
                gridDef = _locationListGrid.GetGridDefinition(url, user),
                _Title = WebLocalizationKeys.LOCATIONS.ToString()
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }

        public JsonResult Locations(GridItemsRequestModel input)
        {
            var userEntityId = _sessionContext.GetUserId();
            var user = _repository.Find<User>(userEntityId);
            var items = _dynamicExpressionQuery.PerformQuery<Location>(input.filters);
            var gridItemsViewModel = _locationListGrid.GetGridItemsViewModel(input.PageSortFilter, items, user);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }

}