
using System.Web.Mvc;

namespace CarDealerApp.App_Data
{
    public static class HtmlImageExtension
    {
        public static MvcHtmlString Image(this HtmlHelper helper,
                                        string imageURL,
                                        string alt,
                                        string width = "150px",
                                        string height = "150px")
        {
            TagBuilder tb = new TagBuilder("img");
            tb.AddCssClass("image-thumbnail");
            tb.MergeAttribute("width",width);
            tb.MergeAttribute("height",height);
            tb.MergeAttribute("src",imageURL);
            tb.MergeAttribute("alt",alt);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
    }
}