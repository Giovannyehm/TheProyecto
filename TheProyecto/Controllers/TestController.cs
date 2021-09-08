using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheProyecto.Controllers
{
    public class TestController : Controller
    {
        [Authorize]
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}