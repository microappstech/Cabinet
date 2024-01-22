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
using Radzen;
using Cabinet.Pages.Assisstant;
using Cabinet.Pages.Doctors;
using Cabinet.Service;

namespace Cabinet.Pages.Appointements
{
    public partial class AppointementsComponent:BasePage
    {
        [Inject]
        public AppointmentService appointmentService { get; set; }
        public RadzenDataGrid<Models.Appointment> grid0;
        public List<Models.Appointment> appointments { get; set; }
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
            var items = await appointmentService.GetAll();
            appointments = items.ToList();
        }

        public async Task Ajouter()
        {
            var result = await DialogService.OpenAsync<AddAppointement>("Ajouter un Rendez-vous", new Dictionary<string, object> { });
            await Load();
        }
        public async Task Edit(Models.Appointment appointment)
        {
            var result = await DialogService.OpenAsync<EditAppointement>("Editer un rendez-vous", new Dictionary<string, object> { { "Id", appointment.Id } });
            await Load();
        }
        public async Task Delete(EventArgs eventArgs, Models.Appointment appointment)
        {
            if (await Confirm("Confirmation de suppression", "Etes vous sure de vouloir supprimer ce rendez-vous") == true)
            {
                try
                {
                    var res = await appointmentService.DeleteItem(appointment.Id);
                    if (res)
                    {
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