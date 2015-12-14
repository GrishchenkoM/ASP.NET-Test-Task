using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListGeneration;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index(int pageNum = 0)
        {
            if (Session["List"] == null)
                Session["List"] = new RandomList(StandartQuantity);

            if (pageNum < 0)
                pageNum = 0;

            var model = new ListGenerationModel
            {
                Array = (((IEnumerable<int>)((RandomList)Session["List"]).GetList).Skip(PageSize * pageNum).Take(PageSize).ToList())
            };
            ViewBag.CurrentPageNum = pageNum;
            ViewBag.PagesCount = (int)Math.Ceiling((double)StandartQuantity / PageSize);
            return View(model);
        }

        private const int StandartQuantity = 10000;
        private const int PageSize = 150;
    }
}
