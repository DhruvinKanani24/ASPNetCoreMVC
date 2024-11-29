using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;

namespace Webgentle.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public ActionResult Index()
        {
            var newBookAlert = new NewBookAlertConfig();
            configuration.Bind("NewBookAlert", newBookAlert);

            bool isDisplay = newBookAlert.DisplayNewBookAlert;
            //var result = configuration.GetValue<bool>("DisplayNewBookAlert");
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
