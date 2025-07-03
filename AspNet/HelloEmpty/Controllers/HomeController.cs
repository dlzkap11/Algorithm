using HelloEmpty.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloEmpty.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            HelloMessage msg = new HelloMessage()
            {
                Message = "Welcome to Asp.Net MVC!"
            };


            ViewBag.Noti = "input message and click submit";

            return View(msg);
        }

        //Post를 처리하는 Index
        [HttpPost]
        public IActionResult Index(HelloMessage obj)
        {
            ViewBag.Noti = "Message Changed";
            return View(obj);
        }


    }
}
