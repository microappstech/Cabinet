using Microsoft.AspNetCore.Components;
using Cabinet.Models;
using Radzen;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Components.Web;

namespace Cabinet.Pages.Users
{
    public partial class AddUserComponent : BasePage
    {

        public User user { get; set; }
       public IEnumerable<dynamic> roles { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                Navigation.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }

        protected async System.Threading.Tasks.Task Load()
        {
            user = new User();

            var securityGetRolesResult = await Security.GetRoles();
            roles = securityGetRolesResult;
        }
        protected async Task Form0Submit(User args)
        {
            try
            {
                var securityCreateUserResult = await Security.CreateUser(args);
            }
            catch (System.Exception ex)
            {
                
            }
        }
        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
