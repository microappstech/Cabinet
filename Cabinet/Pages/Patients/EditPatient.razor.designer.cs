using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Cabinet.Pages.Patients
{
    public partial class EditPatientComponent : BasePage
    {
        [Parameter] public int Id { get; set; }
        [Inject] PatientService patientService { get; set; }
        public Models.Patient patient { get; set; }

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
            patient = await patientService.GetItemById(Convert.ToInt32(Id));
        }
        public async Task Submit(Models.Patient patient)
        {

            try
            {

                var result = await patientService.UpdateItem(patient);
                if(result)
                {
                    await InvokeAsync(StateHasChanged);
                    DialogService.Close();
                }
                Notify(NotificationSeverity.Success, "l'édition terminé avec succès", "Succès");
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
