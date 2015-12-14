using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString PagingNavigator(this HtmlHelper helper, int pageNum)
        {
            var sb = new StringBuilder();
            sb.Append(helper.ActionLink("<<<<<<<", "Index", new {pageNum = pageNum - 1}));
            sb.Append(helper.ActionLink(">>>>>>>", "Index", new {pageNum = pageNum + 1}));
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}