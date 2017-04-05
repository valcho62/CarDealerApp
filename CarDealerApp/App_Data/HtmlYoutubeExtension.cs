using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealerApp.App_Data
{
    public static class HtmlYoutubeExtension
    {
        public static MvcHtmlString YouTube(this HtmlHelper helper,string videoId,
            string width = "560px",string height = "315px",
            string frameborder = "0", string allowfullscreen = "allowfullscreen")
        {
            TagBuilder tb = new TagBuilder("iframe");
            tb.MergeAttribute("src", $"https://www.youtube.com/embed/{videoId}");
            tb.MergeAttribute("width", width);
            tb.MergeAttribute("height", height);
            tb.MergeAttribute("frameborder", frameborder);
            tb.MergeAttribute("allowfullscreen", allowfullscreen);

            return new MvcHtmlString(tb.ToString(TagRenderMode.Normal));
        }
    }
}