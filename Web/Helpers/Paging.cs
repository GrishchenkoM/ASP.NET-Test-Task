using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
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
                    sb.Append(helper.ActionLink(pagesCount.ToString(), "Index", new { pageNum = pagesCount - 1 }));
                    sb.Append("  .  ");
                    sb.Append(helper.ActionLink("Next >>", "Index", new { pageNum = pageNum + 1 }));
                }
                else
                {
                    sb.Append(pagesCount.ToString());
                }
            
            return MvcHtmlString.Create(sb.ToString());
        }

        // Was created for Ajax realization. Not working
        public static MvcHtmlString AjaxPagingNavigator(this AjaxHelper helper, int pageNum, int pagesCount)
        {
            var sb = new StringBuilder();
            AjaxOptions options = new AjaxOptions();
            options.UpdateTargetId = "numberList";
            options.Confirm = "Are you crazy?";
            
            if (pageNum < 0)
                pageNum = 0;

            if (pageNum >= 0)
                if (pageNum > 0)
                {
                    sb.Append(helper.ActionLink("<< Prev", "GetContent", new { pageNum = pageNum - 1 }, options));
                    sb.Append("  .  ");
                    sb.Append(helper.ActionLink("1", "GetContent", new { pageNum = 0 }, options));

                    AddCurrentPage(sb, pageNum, pagesCount - 1);
                }
                else
                {
                    sb.Append("\"1\"");
                    sb.Append(" ...  ");
                }

            if (pageNum < pagesCount - 1)
            {
                sb.Append(helper.ActionLink(pagesCount.ToString(), "GetContent", new { pageNum = pagesCount - 1 }, options));
                sb.Append("  .  ");
                sb.Append(helper.ActionLink("Next >>", "GetContent", new { pageNum = pageNum + 1 }, options));
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
                sb.Append("\"" + (pageNum + 1) + "\"");
                sb.Append(" ...  ");
            }
            else
            {
                sb.Append("  ...  ");
            }
        }
    }
}