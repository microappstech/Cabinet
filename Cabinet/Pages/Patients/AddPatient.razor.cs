using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;

namespace Cabinet.Pages.Patients
{
    public partial class AddPatientComponent : BasePage
    {
        [Inject] PatientService patientService { get; set; }
        public Models.Patient patient { get; set; }
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

        public async Task Load()
        {
            patient = new Models.Patient() { };
        }
        public async Task Submit(Models.Patient item)
        {
            try
            {
                User us = new User()
                {
                    RoleNames = new string[] { "PATIENT" },
                    FullName = patient.FullName,
                };
                //var res = await Security.CreateUser(us);
                //patient.UserId = res.Id;
                var result = await patientService.CreateItem(patient);
                await InvokeAsync(StateHasChanged);
                DialogService.Close();

                Notify(NotificationSeverity.Success, "Création terminé avec succès", "Succès");
                DialogService.Close();
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
