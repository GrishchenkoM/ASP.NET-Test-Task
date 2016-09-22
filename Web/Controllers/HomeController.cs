using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ListGeneration;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["List"] == null)
                Session["List"] = new RandomList(StandartQuantity);
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(int pageNum = 1)
        {
            Session["List"] = new RandomList(StandartQuantity);
            
            return View();
        }

        public ActionResult GetContent(int pageNum = 1)
        {
            if (Session["List"] == null)
                Session["List"] = new RandomList(StandartQuantity);

            if (pageNum < 0)
                pageNum = 0;

            var model = new ViewModel();
            FillModel(model, pageNum);

            ViewBag.CurrentPageNum = pageNum;
            ViewBag.PagesCount = (int)Math.Ceiling((double)StandartQuantity / PageSize);

            try
            {
                var itemsPerPages = model.Array.Skip((pageNum - 1) * PageSize).Take(PageSize);
                var pageInfo = new PageInfo { PageNumber = pageNum, PageSize = PageSize, TotalItems = model.Array.Count() };
                model.PageInfo = pageInfo;
                model.Array = itemsPerPages.ToList();
            }
            catch { }

            return View("IndexContent", model);
        }

        private void FillModel(ViewModel model, int pageNum = 0)
        {
            model.Array =
                (((IEnumerable<int>) ((RandomList) Session["List"]).GetList)).ToList();
        }

        private const int StandartQuantity = 10000;
        private const int PageSize = 150;
    }
}
