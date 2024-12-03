using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;
using Webgentle.BookStore.Repository;
using Webgentle.BookStore.Service;

namespace Webgentle.BookStore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IConfiguration configuration;
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookConfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration, 
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService)
        {
            _newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            _thirdPartyBookConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }

        //public HomeController(IConfiguration _configuration)
        //{
        //    configuration = _configuration;
        //}
        public async Task<ActionResult> Index()
        {

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { "test@gmail.com"},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}","Dhruvin")
                }
            };

            //await _emailService.SendTestEmail(options);

            //var newBookAlert = new NewBookAlertConfig();
            //configuration.Bind("NewBookAlert", newBookAlert);

            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            bool isDisplay = _newBookAlertConfiguration.DisplayNewBookAlert;
            bool isDisplay1 = _thirdPartyBookConfiguration.DisplayNewBookAlert;
            //var result = configuration.GetValue<bool>("DisplayNewBookAlert");
            //var value = _messageRepository.GetName();
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult ContactUs()
        {
            return View();
        }
    }
}
