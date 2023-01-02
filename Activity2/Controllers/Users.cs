using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Activity2.Controllers
{
    public class Users : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserInfos(string fullName, int age)
        {
            ViewBag.FullName = fullName;
            ViewBag.Age = age;
            return View();
        }
    }
}
