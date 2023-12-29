﻿using Cabinet.Service;
using Cabinet.Models;
using Microsoft.AspNetCore.Components;

namespace Cabinet.Pages.Doctors
{
    public partial class AddDoctorComponent : BasePage
    {
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
            doctor = new Models.Doctor() { };
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
