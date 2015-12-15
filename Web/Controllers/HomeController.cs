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
        public ActionResult Index(int pageNum = 0)
        {
            if (Session["List"] == null)
                Session["List"] = new RandomList(StandartQuantity);

            if (pageNum < 0)
                pageNum = 0;

            var model = new ListGenerationModel();
            FillModel(model, pageNum);

            ViewBag.CurrentPageNum = pageNum;
            ViewBag.PagesCount = (int)Math.Ceiling((double)StandartQuantity / PageSize);

            // Was created for Ajax realization. Not working
            if (Request.IsAjaxRequest())
                return PartialView("IndexContent", model);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index()
        {
            Session["List"] = new RandomList(StandartQuantity);

            var model = new ListGenerationModel();
            FillModel(model);

            ViewBag.CurrentPageNum = 0;
            ViewBag.PagesCount = (int) Math.Ceiling((double) StandartQuantity/PageSize);
            
            return View(model);
        }

        // Was created for Ajax realization. Not working
        public ActionResult GetContent(int pageNum = 0)
        {
            if (Session["List"] == null)
                Session["List"] = new RandomList(StandartQuantity);

            if (pageNum < 0)
                pageNum = 0;

            var model = new ListGenerationModel();
            FillModel(model, pageNum);

            ViewBag.CurrentPageNum = pageNum;
            ViewBag.PagesCount = (int)Math.Ceiling((double)StandartQuantity / PageSize);
            return View("IndexContent", model);
        }

        private void FillModel(ListGenerationModel model, int pageNum = 0)
        {
            model.Array =
                (((IEnumerable<int>) ((RandomList) Session["List"]).GetList)
                    .Skip(PageSize*pageNum)
                    .Take(PageSize)
                    .ToList());
        }

        private const int StandartQuantity = 10000;
        private const int PageSize = 150;
    }
}
