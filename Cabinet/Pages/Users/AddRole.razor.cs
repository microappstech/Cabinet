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
    public partial class AddRoleComponent:BasePage
    {
        public IdentityRole role { get; set; }

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
            role = new IdentityRole();
        }

        protected async System.Threading.Tasks.Task Form0Submit(IdentityRole args)
        {
            try
            {
                var securityCreateRoleResult = await Security.CreateRole(args);
                Navigation.NavigateTo("application-roles");
            }
            catch (System.Exception ex)
            {
                //notificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Cannot create role", Detail = $"{securityCreateRoleException.Message}" });
            }
        }

        protected async System.Threading.Tasks.Task Close(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}