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
using Cabinet.Service;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace Cabinet.Pages.Doctors
{
    public partial class DoctorComponent  : BasePage
    {
        public IEnumerable<Cabinet.Models.Doctor> Doctors { get; set; }
        public RadzenDataGrid<Models.Doctor> grid0;
        [Inject] protected DoctorService doctorService { get; set; }    
        protected override async Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if(!Security.IsAuthenticated())
            {
                Navigation.NavigateTo("login",true);
            }
            else
            {
                await Load();
            }
        }
        public async Task Load()
        {
            Doctors = await doctorService.GetDoctors();

        }
        public async Task Ajouter()
        {
            var result = await DialogService.OpenAsync<AddDoctor>("Add Doctor", new Dictionary<string, object> { });
        }
        public async Task Edit(Models.Doctor doctor)
        {
            var result = await DialogService.OpenAsync<EditDoctor>("Edit Doctor", new Dictionary<string, object> { { "Id", doctor.Id } });
        }
        public async Task Delete(EventArgs eventArgs, Models.Doctor doctor)
        {
            if(await Confirm("Confirmation de suppression","Etes vous sure de vouloir supprimer ce doctor") == true)
            {
                try
                {
                    var res = await doctorService.DeleteDoctor(doctor.Id);
                    if (res) {
                        Notify(Radzen.NotificationSeverity.Success, "Succès", "Suppression terminé avec succès");
                    }
                    else
                    {
                        Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                    }
                    await Load();
                }
                catch (Exception ex)
                {
                    Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                }
            }
        }
    }
}