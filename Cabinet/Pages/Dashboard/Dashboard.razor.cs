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

namespace Cabinet.Pages.Dashboard
{
    public partial class DashboardComponent : BasePage
    { 
        public int nbPatients { get; set; }
        public int nbDoctors { get; set; }
        public int nbInfirmier { get; set; }
        public int nbAppoitement { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                Navigation.NavigateTo("login", true);
            }
            else
            {
                await Load();   
            }
        }
        public async Task Load()
        {
            nbDoctors = await generalService.GetNbDoctors();
            nbInfirmier = await generalService.GetNbInfirmiers();
            nbPatients = await generalService.GetNbPatient();
            nbAppoitement = await generalService.GetNbAppoitemet();






        }
    }
}