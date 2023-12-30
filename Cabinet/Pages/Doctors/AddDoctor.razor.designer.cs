using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;

namespace Cabinet.Pages.Doctors
{
    public partial class AddDoctorComponent : BasePage
    {
        [Inject] DoctorService doctorService { get; set; }
        public Models.Doctor doctor { get; set; }
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
            doctor = new Models.Doctor() { };
        }
        public async Task Submit(Models.Doctor doctor)
        {
            try
            {
                User us = new User()
                {
                    RoleNames = new string[] { "DOCTOR" },
                    FullName = doctor.FullName,
                    Photo = doctor.Photo
                };
                var res = await Security.CreateUser(us);
                doctor.UserId = res.Id;
                doctor.UserName = res.UserName;
                var result = await doctorService.CreateDoctor(doctor);
                await InvokeAsync(StateHasChanged);
                DialogService.Close();
        
                Notify(NotificationSeverity.Success, "Création termné avec succès", "Succès");
                DialogService.Close();
            }catch(Exception e)
            {
                Notify(NotificationSeverity.Error, "Echec ","quelque chose n'est pas correct");
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
