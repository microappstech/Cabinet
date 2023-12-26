using Cabinet.Data;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<bool> InitializeAsync(AuthenticationStateProvider authenticationStateProvider)
        {
            var auth = await authenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = auth.User;
            var name = Principal.Identity.Name;
            if(user == null &&  name != null)
            {

            }
            return false;

        }

    }
}
