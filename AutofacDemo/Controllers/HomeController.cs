using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutofacDemo.Controllers
{
    public class HomeController : Controller
    {
        ISalesService _service;
        IAppEvents _events;

        public HomeController(ISalesService service, IAppEvents events)
        {
            _service = service;
            _events = events;
        }

        public ActionResult Index()
        {
            _events.Raise<string>();
            return View();
        }
    }
}