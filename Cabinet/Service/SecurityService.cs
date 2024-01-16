using Cabinet.Data;
using Cabinet.Models;
using Cabinet.Pages.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Cabinet.Service
{
    public class SecurityService
    {
        public event Action Authenticated;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly NavigationManager _navigation;

        public ApplicationDbContext context { get; private set; }
        public SecurityService(
            ApplicationDbContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            NavigationManager navigation
            ){
            _signInManager = signInManager;
            _userManager = userManager; 
            _roleManager = roleManager;
            _navigation = navigation;
            this.context = context;
        }
        public ClaimsPrincipal Principal { get; set; }

        User user;
        public User User
        {
            get
            {
                if (user == null)
                {
                    return new User() { FullName = "Anonymous" };
                }

                return user;
            }
        }
        public bool IsAuthenticated()
        {
            return Principal != null ? Principal.Identity.IsAuthenticated : false;
        }
        static SemaphoreSlim Slim = new SemaphoreSlim(1,1);
        public async Task<bool> InitializeAsync(AuthenticationStateProvider authenticationStateProvider)
        {
            var auth = await authenticationStateProvider.GetAuthenticationStateAsync();
            Principal = auth.User;
            var name = Principal.Identity.Name;
            if(user == null &&  name != null)
            {
                await Slim.WaitAsync();
                try
                {
                    user =await _userManager.FindByNameAsync(name);
                    if(user != null)
                    {
                        user.RoleNames =await _userManager.GetRolesAsync(user);
                    }
                }
                finally
                {
                    Slim.Release();
                }

            }
            var result = IsAuthenticated();
            if (result)
            {
                Authenticated?.Invoke();
            }
            return result;
        }

        public bool IsInRole(string[] roles)
        {
            //if (roles.Contains("Everybody"))
            //{
            //    return true;
            //}
            
            if (!IsAuthenticated())
            {
                return false;
            }

            if (roles.Contains("Authenticated"))
            {
                return true;
            }

            var r =  roles.Any(role => Principal.IsInRole(role));
            return r;
        }
        public async Task<User> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                
                if (user != null)
                {
                    context.Entry(user).Reload();
                    user.RoleNames = await _userManager.GetRolesAsync(user);
                }

                return await Task.FromResult(user);
            }
            finally
            {
            }
        }


        public async Task<User> CreateUser(User user)
        {
            var result = await _userManager.CreateAsync(user, user.Password);



            var roles = user.RoleNames;

            if (roles != null && roles.Any())
            {
                result = await _userManager.AddToRolesAsync(user, roles);
            }

            user.RoleNames = roles;


            return user;
        }
        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await Task.FromResult(_roleManager.Roles);
        }

        public async Task<IdentityRole> CreateRole(IdentityRole role)
        {
            var result = await _roleManager.CreateAsync(role);


            return role;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var items = context.Users.AsNoTracking();
            return await Task.FromResult(items);
        }
        public async Task Logout(string RedirectTo = null)
        {
            _navigation.NavigateTo("Auth/Logout", true);
         
        }
        public async Task<string> ReInitPassword(User user)
        {
            var result = await _userManager.RemovePasswordAsync(user);
            string Password = "Cab"+ DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Second.ToString() + "!_" + DateTime.UtcNow.Millisecond.ToString()+"#";
            result = await _userManager.AddPasswordAsync(user,Password);
            if (result.Succeeded)
            {
                return await Task.FromResult(Password);
            }
            return null;
        }
    }
}
