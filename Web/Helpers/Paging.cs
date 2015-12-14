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
        public static MvcHtmlString PagingNavigator(this HtmlHelper helper, int pageNum, int pagesCount)
        {
            var sb = new StringBuilder();

            if (pageNum < 0)
                pageNum = 0;

            if (pageNum >= 0)
                if (pageNum > 0)
                {
                    sb.Append(helper.ActionLink("<< Prev", "Index", new { pageNum = pageNum - 1 }));
                    sb.Append("  .  ");
                    sb.Append(helper.ActionLink("1", "Index", new { pageNum = 0 }));

                    AddCurrentPage(sb, pageNum, pagesCount - 1);
                }
                else
                {
                    sb.Append("1");
                    sb.Append(" ...  ");
                }

                if (pageNum < pagesCount - 1)
                {
                    sb.Append(helper.ActionLink(pagesCount.ToString(), "Index", new {pageNum = pagesCount - 1}));
                    sb.Append("  .  ");
                    sb.Append(helper.ActionLink("Next >>", "Index", new {pageNum = pageNum + 1}));
                }
                else
                {
                    sb.Append(pagesCount.ToString());
                }
            
            return MvcHtmlString.Create(sb.ToString());
        }

        private static void AddCurrentPage(StringBuilder sb, int pageNum, int lastPage)
        {
            if (pageNum != 0 && pageNum != lastPage)
            {
                sb.Append("  ... ");
                sb.Append((pageNum + 1).ToString());
                sb.Append(" ...  ");
            }
            else
            {
                sb.Append("  ...  ");
            }
        }
    }
}