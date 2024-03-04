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
using Cabinet.Models;

namespace Cabinet.Pages.Patients
{
    public partial class PatientsComponent  : BasePage
    {
        public IEnumerable<Cabinet.Models.Patient> patients { get; set; }
        public RadzenDataGrid<Models.Patient> grid0;
        [Inject] protected PatientService patientService{ get; set; }    
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
            patients = await patientService.GetAll();

        }
        public async Task Ajouter()
        {
            var result = await DialogService.OpenAsync<AddPatient>("Ajouter Patient", new Dictionary<string, object> { });
        }
        public async Task Edit(Models.Patient patient)
        {
            var result = await DialogService.OpenAsync<EditPatient>("Edit patient", new Dictionary<string, object> { { "Id", patient.Id } });
        }
        public async Task Detail(Models.Patient patient)
        {
            var result = await DialogService.OpenAsync<PatientDetail>("Details de patient", new Dictionary<string, object> { { "Id", patient.Id } }, new Radzen.DialogOptions { Draggable=true, Bottom = "0px", Left ="0px", Style="padding:0px; margin:0px; background-color:orange;"});
        }        
        public async Task Delete(EventArgs eventArgs, Models.Patient patient)
        {
            if(await Confirm("Confirmation de suppression","Etes vous sure de vouloir supprimer cette patient") == true)
            {
                try
                {
                    var res = await patientService.DeleteItem(patient.Id);
                    if (res) {
                        Notify(Radzen.NotificationSeverity.Success, "Succès", "Suppression terminé avec succès");
                    }
                    else
                    {
                        Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                    }
                    await grid0.Reload();
                }
                catch (Exception ex)
                {
                    Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                }
            }
        }
        public async Task Statistic(Models.Patient patient)
        {
            var result = await DialogService.OpenAsync<Cabinet.Pages.StatisticPoitements>("Statistique de patient", new Dictionary<string, object> { { "Id", patient.Id } }, new Radzen.DialogOptions { Draggable = true, Bottom = "0px", Left = "0px", Style = "padding:0px; margin:0px; " });
        }
    }
}