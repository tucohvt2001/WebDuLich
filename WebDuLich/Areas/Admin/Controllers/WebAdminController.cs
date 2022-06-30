using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDuLich.Models.Entities;

namespace WebDuLich.Areas.Admin.Controllers
{
    public class WebAdminController : Controller
    {
        // GET: Admin/WebAdmin
        public ActionResult Index()
        {
                return View();
        }
        /*public ActionResult ServerTime()
        {
            var text = DateTime.Now.ToString("HH:mm:ss tt");
            return Content(text);
        }*/
    }
}