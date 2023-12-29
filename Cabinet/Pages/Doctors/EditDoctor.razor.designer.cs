using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;

namespace Cabinet.Pages.Doctors
{
    public partial class EditDoctorComponent : BasePage
    {
        [Parameter] public int Id { get; set; }
        [Inject] DoctorService doctorService { get; set; }
        public Models.Doctor doctor { get; set; }
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
            var result = await doctorService.CreateDoctor(doctor);
            if(result == null)
            {
                await InvokeAsync(StateHasChanged);
                DialogService.Close();
            }
        }
    }
}
