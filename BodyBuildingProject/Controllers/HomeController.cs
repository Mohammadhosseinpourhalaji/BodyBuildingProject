using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BodyBuildingProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //TempData["msg"] = "hiii";
            return View();
        }
    }
}