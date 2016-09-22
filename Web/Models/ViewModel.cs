using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class ViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<int> Array { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int) Math.Ceiling((decimal) TotalItems/PageSize); }
        }
    }
}