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
        IAppEvents _myService;

        public HomeController(ISalesService service, IAppEvents myService)
        {
            _service = service;
            _myService = myService;
        }

        public ActionResult Index()
        {
            _myService.Raise<string>();
            return View();
        }
    }
}