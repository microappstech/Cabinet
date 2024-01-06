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
using Radzen.Blazor;
using Cabinet.Models;
using System.Reflection.Metadata;
using Cabinet.Service;
using Microsoft.AspNetCore.Mvc;
using Radzen;

namespace Cabinet.Pages.Patients
{
    public partial class PatientDetailComponent : BasePage
    { 
        [Parameter] public int Id { get; set; }
        public Models.Patient patient { get; set; }
        public Models.Appointment appointment { get;set; }
        [Inject] public PatientService patientService {  get; set; }
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
            
            var r    = await patientService.GetItemById(Convert.ToInt32(Id));
            patient = r;
        }
    }
}