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
using Cabinet.Service;
using Cabinet.Models;

namespace Cabinet.Pages.Appointements
{
    public partial class EditAppointementComponent : BasePage
    {
        [Parameter] public int Id { get; set; }
        public Models.Appointment appointment { get; set; }
        public IEnumerable<Patient> patients { get; set; }
        public IEnumerable<Doctor> doctors { get; set; }
        [Inject] public AppointmentService appointmentService { get; set; }
        [Inject] public PatientService patientService { get; set; }
        [Inject] public DoctorService doctorService { get; set; }

        public int fileSize { get; set; }
        public string ErrorMsg { get; set; }
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
        protected async Task Load()
        {
            appointment = await appointmentService.GetById(Convert.ToInt32(Id));
            patients = await patientService.GetAll();
            doctors = await doctorService.GetDoctors();
        }

        public async Task Submit(Models.Appointment appointment)
        {
            try
            {
                var result = await appointmentService.UpdateItem(appointment);
                if(result)
                {
                    Notify(NotificationSeverity.Success, "Edition terminé avec succès", "Succès");
                    DialogService.Close();
                }
            }
            catch (Exception e)
            {
                Notify(NotificationSeverity.Error, "Echec ", "quelque chose n'est pas correct");
            }
        }

        public void OnChange(string value, string name)
        {

        }
        public void OnError(UploadErrorEventArgs args, string name)
        {
            if (args.Message.ToLower().Contains("too large"))
            {
                ErrorMsg = "la taille d'image dépasse la taille autorisé";
            }

        }
    }
}