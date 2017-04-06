using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarDealerApp.App_Start
{
    public static class HtmlTableExtention
    {
        public static MvcHtmlString Table<T>(this HtmlHelper helper,
            IEnumerable<T> models,
            params string[] tableCSSs)
        {
            var paramNames = typeof(T).GetProperties().Select(x => x.Name).ToArray();
            var table = new TagBuilder("table");
            var tableInner = new StringBuilder();
            foreach (var tableCSS in tableCSSs)
            {
                table.AddCssClass(tableCSS);
            }
            // blok header
            var tableHeader = new TagBuilder("tr");
            StringBuilder headerBuild = new StringBuilder();
            foreach (var paramName in paramNames)
            {
                var headerInner = new TagBuilder("th");
                headerInner.InnerHtml = paramName;
                headerBuild.Append(headerInner);
            }
            tableHeader.InnerHtml = headerBuild.ToString();
            tableInner.Append(tableHeader);
            // blok data
           
            foreach (var model in models)
            {
                var tableBody = new TagBuilder("tr");
                var bodyBuild = new StringBuilder();
               
                foreach (var parameter in paramNames)
                {
                    var bodyInner = new TagBuilder("td");
                    bodyInner.InnerHtml = typeof(T).GetProperty(parameter).GetValue(model).ToString();
                    bodyBuild.Append(bodyInner);
                }
                tableBody.InnerHtml = bodyBuild.ToString();
                tableInner.Append(tableBody);
            }
            table.InnerHtml = tableInner.ToString();

            return new MvcHtmlString(table.ToString());
        }
    }
}