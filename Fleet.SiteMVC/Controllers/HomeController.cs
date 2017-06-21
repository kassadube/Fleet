using Fleet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fleet.SiteMVC
{
    public class HomeController : Controller
    {
        public HomeController(IAccountRepository _repository )
        {
            
            _repository.GetAccountDetails(4319);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IAccountRepository _rep = DependencyResolver.Current.GetService<IAccountRepository>();
            _rep.GetAccountDetails(4319);
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}