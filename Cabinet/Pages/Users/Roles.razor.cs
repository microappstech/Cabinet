using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Cabinet;
using Cabinet.Shared;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Cabinet.Pages.Users
{
    public partial class RolesComponent:BasePage
    {
        public IEnumerable<IdentityRole> roles { get; set; }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
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
            var securityGetRolesResult = await Security.GetRoles();
            roles = securityGetRolesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddRole>("Add Application Role", null);
            await Load();
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, IdentityRole data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    //var securityDeleteRoleResult = await Security.DeleteRole($"{data.Id}");
                    await Load();
                    //if (securityDeleteRoleResult != null)
                    //{
                    //    //await grid0.Reload();
                    //}
                }
            }
            catch (System.Exception securityDeleteRoleException)
            {
                //notificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to delete role" });
            }
        }
    }
}