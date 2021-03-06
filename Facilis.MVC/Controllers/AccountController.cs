﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Facilis.Infra.CrossCutting.Identity.Configuration;
using Facilis.Infra.CrossCutting.Identity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using System.Collections.Generic;
using AutoMapper;
using System;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private readonly IEstadoAppService _estadoApp;
        private readonly ICidadeAppService _cidadeApp;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IEstadoAppService estadoApp, ICidadeAppService cidadeApp)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _estadoApp = estadoApp;
            _cidadeApp = cidadeApp;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome");
            ViewBag.CidadeId = new SelectList(string.Empty, "CidadeId", "Nome");
            CarregarDropDownSexo();
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    usuario = new Usuario
                    {
                        Bairro = model.Bairro,
                        Cep = model.Cep,
                        Complemento = model.Complemento,
                        DataNascimento = model.DataNascimento,
                        Endereco = model.Endereco,
                        Nome = model.Nome,
                        Numero = model.Numero,
                        Sexo = model.Sexo,
                        Telefone = model.Telefone,
                        Sobrenome = model.Sobrenome,
                        EstadoId = model.EstadoId,
                        CidadeId = model.CidadeId,
                        Estado = model.Estado,
                        Cidade = model.Cidade,
                        Email = model.Email
                    }
                };

                //para permitir relacionamento de usuario com outras entidades
                user.usuario.Id = user.Id;

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome", model.EstadoId);
                ViewBag.CidadeId = new SelectList(_cidadeApp.ListarPorEstado(model.EstadoId).ToList(), "CidadeId", "Nome", model.CidadeId);
                CarregarDropDownSexo();
                AddErrors(result);


            }

            ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome", model.EstadoId);
            ViewBag.CidadeId = new SelectList(_cidadeApp.ListarPorEstado(model.EstadoId).ToList(), "CidadeId", "Nome", model.CidadeId);
            CarregarDropDownSexo();

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult Edit(string id)
        {

            var user = _userManager.FindById(id);
            var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(user.usuario);
            ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome", user.usuario.EstadoId);
            ViewBag.CidadeId = new SelectList(_cidadeApp.ListarPorEstado(user.usuario.EstadoId).ToList(), "CidadeId", "Nome", user.usuario.CidadeId);
            CarregarDropDownSexo();

            return View(usuarioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    usuario = new Usuario
                    {
                        Bairro = model.Bairro,
                        Cep = model.Cep,
                        Complemento = model.Complemento,
                        DataNascimento = model.DataNascimento,
                        Endereco = model.Endereco,
                        Nome = model.Nome,
                        Numero = model.Numero,
                        Sexo = model.Sexo,
                        Telefone = model.Telefone,
                        Sobrenome = model.Sobrenome,
                        EstadoId = model.EstadoId,
                        CidadeId = model.CidadeId,
                        Estado = model.Estado,
                        Cidade = model.Cidade,
                        Email = model.Email
                    }
                };

                //para permitir relacionamento de usuario com outras entidades
                user.usuario.Id = user.Id;

                _userManager.ChangePhoneNumber(user.Id, user.usuario.Telefone.ToString(), null);

                var result = await _userManager.UpdateAsync(user);


                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome", model.EstadoId);
                ViewBag.CidadeId = new SelectList(_cidadeApp.ListarPorEstado(model.EstadoId).ToList(), "CidadeId", "Nome", model.CidadeId);
                CarregarDropDownSexo();
                //AddErrors(result);


            }

            ViewBag.EstadoId = new SelectList(_estadoApp.GetAll().OrderBy(e => e.Nome), "EstadoId", "Nome", model.EstadoId);
            ViewBag.CidadeId = new SelectList(_cidadeApp.ListarPorEstado(model.EstadoId).ToList(), "CidadeId", "Nome", model.CidadeId);
            CarregarDropDownSexo();

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id.ToString());
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);


                //enviar e-mail confirmando que a inscrição for realizada
                var assunto = "Esqueci minha senha";
                var mensagem = string.Format("Para alterar sua senha clique <a href='{0}'>aqui</a>", callbackUrl);
                var destinatario = new List<string>();
                destinatario.Add(model.Email);

                await EmailService.SendAsync(assunto, mensagem, destinatario);

                return View("ForgotPasswordConfirmation");
            }

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Não revelar se o usuario nao existe ou nao esta confirmado
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id.ToString(), model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }

        }

        [AllowAnonymous]
        public ActionResult ListarCidadesPorEstado(int estadoId) => Json(_cidadeApp.ListarPorEstado(estadoId).ToList(), JsonRequestBehavior.AllowGet);

        private void CarregarDropDownSexo()
        {
            var SexoList = new List<dynamic>();
            SexoList.Add(new { Id = "", Text = "" });
            SexoList.Add(new { Id = "M", Text = "Masculino" });
            SexoList.Add(new { Id = "F", Text = "Feminino" });
            ViewBag.SexoList = new SelectList(SexoList, "Id", "Text");

        }
        #endregion
    }
}