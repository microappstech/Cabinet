using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Cabinet.Pages.Doctors
{
    public partial class EditDoctorComponent : BasePage
    {
        [Parameter] public int Id { get; set; }
        [Inject] DoctorService doctorService { get; set; }
        public Models.Doctor doctor { get; set; }

        public string ErrorMsg { get; set; }
        public string fileName;
        public long? fileSize;
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
            doctor = await doctorService.GetDoctorById(Convert.ToInt32(Id));
        }
        public async Task Submit(Models.Doctor doctor)
        {

            try
            {

                var result = await doctorService.UpdateDoctor(doctor);
                if(result)
                {
                    await InvokeAsync(StateHasChanged);
                    DialogService.Close();
                }
                Notify(NotificationSeverity.Success, "Création termné avec succès", "Succès");
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
            if(args.Message.ToLower().Contains("too large"))
            {
                ErrorMsg = "la taille d'image dépasse la taille autorisé";
            }

        }
    }
}
