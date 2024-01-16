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

namespace Cabinet.Controlles
{
    [Route("[controller]/[action]")]
    public partial class AuthController : Controller
    {

        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DoctorService doctorService;
        private readonly IWebHostEnvironment env;

        SecurityService Security;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IWebHostEnvironment env, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DoctorService doctorService,
        SecurityService _Security, ILogger<AuthController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.env = env;
            this.doctorService = doctorService;
            
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
        public async Task<IActionResult> Login(string userName, string password, string redirectUrl)
        {



            if (env.EnvironmentName == "Development" && userName == "admin" && password == "admin")
            {
                var claims = new List<Claim>()
                {
                        new Claim(ClaimTypes.Name, "ADMIN"),
                        new Claim(ClaimTypes.Email, "admin"),
                        new Claim(ClaimTypes.Role, "ADMIN"),
                };

                roleManager.Roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));
                await signInManager.SignInWithClaimsAsync(new User { UserName = userName, Email = userName }, isPersistent: false, claims);

                return Redirect($"~/{redirectUrl}");
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {

                var user = await userManager.FindByNameAsync(userName);

                var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

                if (result.Succeeded)
                {


                    user.IsConnected = true;


                    //if (user.ch != null && user.requierpwdchange.Value)
                    //{

                    //    return Redirect($"~/Profile");

                    //}
                    user = await Security.GetUserById(user.Id);
                    user.IsConnected = true;


                    return Redirect($"~/{redirectUrl}");
                }
            }

            return RedirectWithError("Utilisateur ou mot de passe invalide", redirectUrl);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string fullname, string email, string specalite, string phone, string cin, string photo, string password )
        {

            //using(var scope = new TransactionScope())
            //{
                var user = new User { UserName = email, Email = email };
                
                var resultUser = await userManager.CreateAsync(user, password);
                var _user = await userManager.FindByNameAsync(user.Email);
                if (resultUser.Succeeded)
                {
                    string[] roles = new string[] { "DOCTOR" };
                    var r = await roleManager.FindByNameAsync("DOCTOR");
                    if (roles != null && roles.Any())
                    {
                        var x = await userManager.AddToRoleAsync(_user, "DOCTOR"); // .AddToRolesAsync(user, roles);
                    }
                    Models.Doctor doctor = new Models.Doctor()
                    {
                        FullName = fullname,
                        UserName = email,
                        CIN = cin,
                        Specialite = specalite,
                        Photo = photo,
                        PhoneNumber = phone,
                        UserId = user.Id
                    };
                    var resultDoctor = await doctorService.CreateDoctor(doctor);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    if (resultDoctor != null)
                    {
                        //scope.Complete();
                        return Redirect("/");
                    }
                    
                    var msg = string.Join(", ", resultUser.Errors.Select(error => error.Description));
                    return Redirect($"~/Register?error={msg}");
                }
                
                var message = string.Join(", ", resultUser.Errors.Select(error => error.Description));
                //scope.Dispose();
                return Redirect($"~/Register?error={message}");
                

            //}

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null)
            {
                return Redirect($"~/Profile?error=Utilisateur ou mot de passe invalide");
            }

            var id = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await userManager.FindByIdAsync(id);

            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                user = await Security.GetUserById(user.Id);
                await signInManager.SignInAsync(user, isPersistent: true);
                return Redirect("~/");
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return Redirect($"~/Profile?error={message}");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string RedirectTo = null)
        {


            var id = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user = await Security.GetUserById(user.Id);

            }
            await HttpContext.SignOutAsync();

            await signInManager.SignOutAsync();
            if (RedirectTo != null && RedirectTo.Contains("resulteleve"))
            {
                return Redirect("~/resulteleve");
            }
            else
            {
                return Redirect("~/");
            }

        }

    }
}
