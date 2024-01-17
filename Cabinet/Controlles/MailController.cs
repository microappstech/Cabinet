using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Cabinet.Models;
using Cabinet.Service;
using Cabinet.Pages.Doctors;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Transactions;
using Cabinet.Data;
using MailKit;
using MailKit.Net.Smtp;
//using MimeKit; 

namespace Cabinet.Controlles
{
    [Route("[controller]/[action]")]
    public partial class MailController : Controller
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly Cabinet.Service.EmailService emailService;
        private readonly IWebHostEnvironment env;

        SecurityService Security;
        private readonly ILogger<MailController> _logger;
        public MailController(IWebHostEnvironment env, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DoctorService doctorService,
        SecurityService _Security, ILogger<MailController> logger, EmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.env = env;
            this.emailService = emailService;
            Security = _Security;
            _logger = logger;
        }

        private IActionResult RedirectWithError(string error, string redirectUrl)
        {
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return Redirect($"~/Login?error={error}&redirectUrl={Uri.EscapeDataString(redirectUrl)}");
            }
            else
            {
                return Redirect($"~/Login?error={error}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMailReset(string Mail)
        {
            var user = await userManager.FindByNameAsync(Mail);
            
            if(user == null)
            {
                return Redirect("/forget?message=Aucune compte liéer au ce mail ");
            }
            var password = await Security.ReInitPassword(user);
            
                    var sended = await emailService.sendMailResset(user, password);
            if (sended)
            {
                return Redirect("/forget?message=le nouveau mot passe est bien envoyé au votre courier");
            }

            return Redirect("/forget?message=Aucune compte liées a cette mail ");
        }
    }
}
