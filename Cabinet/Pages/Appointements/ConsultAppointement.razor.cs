
using Radzen.Blazor;
using Radzen;
using Microsoft.AspNetCore.Components;
using Cabinet.Service;

namespace Cabinet.Pages.Appointements
{
    public partial class ConsultAppointementComponent : BasePage
    {
        public RadzenScheduler<Cabinet.Models.Appointment> scheduler;
        public Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        public List<Cabinet.Models.Appointment> appointments { get; set; }
        [Inject] AppointmentService appointmentService { get; set; }


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
            var r = await appointmentService.GetAll();
            appointments = r.ToList();
        }
        public void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,0);";
            }
        }

        public async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            
        }

        public async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<Cabinet.Models.Appointment> args)
        {
            
            
            
            
        }

        public void OnAppointmentRender(SchedulerAppointmentRenderEventArgs<Cabinet.Models.Appointment> args)
        {
            // Never call StateHasChanged in AppointmentRender - would lead to infinite loop
            if (args.Data.Annuled)
            {
                args.Attributes["style"] = "background: red";
            }
            else if (args.Data.Passed)
            {
                args.Attributes["style"] = "background: green";
            }
        }
    }
}