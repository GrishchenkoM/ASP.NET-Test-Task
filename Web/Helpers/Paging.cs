using System;
using System.Text;
using System.Web.Mvc;
using Web.Models;

namespace Web.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                if (pageInfo.PageNumber >= 1)
                    if (pageInfo.PageNumber > 1)
                    {
                        CreateTag("onclick", "UpdateContent(" + (pageInfo.PageNumber - 1) + ")", "<< Prev", pageInfo, result);
                        
                        result.Append("  .  ");

                        CreateTag("onclick", "UpdateContent(" + 1 + ")", "1", pageInfo, result);
                        
                        AddCurrentPage(result, pageInfo.PageNumber, pageInfo.TotalPages);
                    }
                    else
                    {
                        result.Append("\"1\"");
                        result.Append(" ...  ");
                    }

                if (pageInfo.PageNumber < pageInfo.TotalPages)
                {
                    CreateTag("onclick", "UpdateContent(" + pageInfo.TotalPages + ")", pageInfo.TotalPages.ToString(), pageInfo, result);

                    result.Append("  .  ");

                    CreateTag("onclick", "UpdateContent(" + (pageInfo.PageNumber + 1) + ")", "Next >>", pageInfo, result);
                }
                else
                {
                    result.Append(pageInfo.TotalPages.ToString());
                }
            }
            catch { }
            return MvcHtmlString.Create(result.ToString());
        }

        private static void CreateTag(string key, string value, string innerHtml, PageInfo pageInfo, StringBuilder result)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute(key, value);
            tag.InnerHtml = innerHtml;
            result.Append(tag);
        }

        private static void AddCurrentPage(StringBuilder sb, int pageNum, int lastPage)
        {
            if (pageNum != 1 && pageNum != lastPage)
            {
                sb.Append("  ... ");
                sb.Append("\"" + (pageNum) + "\"");
                sb.Append(" ...  ");
            }
            else
            {
                sb.Append("  ...  ");
            }
        }
    }
}