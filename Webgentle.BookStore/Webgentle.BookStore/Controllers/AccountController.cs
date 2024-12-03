using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.BookStore.Models;
using Webgentle.BookStore.Repository;

namespace Webgentle.BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SighUpUserModel usermodel)
        {
            if(ModelState.IsValid)
            {
                var result =  await _accountRepository.CreateUserAsync(usermodel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(usermodel);
                }
                
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = usermodel.Email });
            }

            return View(usermodel);
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnurl)
        {
            if(ModelState.IsValid)
            {
               var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnurl))
                    {
                        return LocalRedirect(returnurl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
                
            }
            return View(signInModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            //await _accountRepository.SignOutAsync();
            return View();
        }

        [HttpPost]
        [Route("change-password")]
        public async  Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if(ModelState.IsValid)
            {
                //var result = await _accountRepository.ChangePasswordAsync(model);
                //if(result.Succeeded)
                //{
                //    ViewBag.IsSuccess = true;
                //    ModelState.Clear();
                //    return View();
                //}
                //foreach(var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}
            }
            //await _accountRepository.SignOutAsync();
            return View(model);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };
            
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                //var result = await _accountRepository.ConfirmEmailAsync(uid,token);
                //if (result.Succeeded)
                //{
                //    model.EmailVerified = true;
                //}
            }

            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            //var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            //if(user!= null)
            //{
            //    if (user.EmailConfirmed)
            //    {
            //        model.EmailVerified = true;
            //        return View();
            //    }

            //    await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
            //    model.EmailSent = true;
            //}
            //else
            //{
            //    ModelState.AddModelError("","something wnet wrong.");
            //}

            return View(model);
        }

        [AllowAnonymous, HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                //if (user != null)
                //{
                //    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                //}

                //ModelState.Clear();
                //model.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                UserId = uid,
                Token = token
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                //var result = await _accountRepository.ResetPasswordAsync(model);
                //if (result.Succeeded)
                //{
                //    ModelState.Clear();
                //    model.IsSuccess = true;
                //    return View(model);
                //}

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}
                
            }
            return View(model);
        }
    }
}
