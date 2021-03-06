using System;
using CC.Core.Reflection;
using CC.UI.Helpers.Configuration;

namespace CC.Core.Html.CCUI.HtmlConventionRegistries
{
    public class CCElementNamingConvention : IElementNamingConvention
    {
        public string GetName(Type modelType, Accessor accessor)
        {
            return accessor.FieldName;
        }
    }
}