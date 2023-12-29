using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
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
        public async Task<bool> InitializeAsync(AuthenticationStateProvider authenticationStateProvider)
        {
            var auth = await authenticationStateProvider.GetAuthenticationStateAsync();
            Principal = auth.User;
            var name = Principal.Identity.Name;
            if(user == null &&  name != null)
            {
                user =await _userManager.FindByNameAsync(name);
                if(user != null)
                {
                    user.RoleNames =await _userManager.GetRolesAsync(user);
                }

            }
            var result = IsAuthenticated();
            if (result)
            {
                Authenticated?.Invoke();
            }
            return result;
        }

        public bool IsInRole(params string[] roles)
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

            return roles.Any(role => Principal.IsInRole(role));
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


            int nbUsers = _userManager.Users.Count() + 1;

            user.UserName = "User_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + nbUsers.ToString() + "_" + DateTime.UtcNow.Millisecond.ToString();  //DateTime.UtcNow.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
            user.Password = user.UserName + '!';// CreateRandomPassword(12);

            
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
        public async Task Logout(string RedirectTo = null)
        {
            _navigation.NavigateTo("Auth/Logout", true);
         
        }
    }
}
