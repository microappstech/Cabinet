using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
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
using Cabinet.Service;

namespace Cabinet.Shared
{
    public partial class MainLayoutComponent: LayoutComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }


        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        protected RadzenBody body0;
        protected RadzenSidebar sidebar0;
        
        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        string _Culture;
        protected string Culture
        {
            get
            {
                return _Culture;
            }

            set
            {
                if (!object.Equals(_Culture, value))
                {
                    _Culture = value;
                    InvokeAsync(() =>
                    {
                        StateHasChanged();
                    });
                }
            }
        }

        private void Authenticated()
        {
            StateHasChanged();
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (Security != null)
            {
                Security.Authenticated += Authenticated;
                await Security.InitializeAsync(AuthenticationStateProvider);
                
            }

            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            //Culture = "";
            //Culture = await JSRuntime.InvokeAsync<string>("Radzen.getCulture");
            
        }


        protected async System.Threading.Tasks.Task SidebarToggle0Click(dynamic args)
        {
            await InvokeAsync(() =>
            {
                sidebar0.Toggle();
            });
            await InvokeAsync(() =>
            {
                body0.Toggle();
            });
        }

        protected async System.Threading.Tasks.Task Profilemenu0Click(dynamic args)
        {
            if (args.Value == "Logout")
            {
                await Security.Logout();
            }
        }

        protected async System.Threading.Tasks.Task logout()
        {
            await Security.Logout();
        }
        
    }
}