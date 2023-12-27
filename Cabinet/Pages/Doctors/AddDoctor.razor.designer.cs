using Cabinet.Service;
using Cabinet.Pages;

namespace Cabinet.Pages.Doctors
{
    public partial class AddDoctorComponent : BasePage
    {
        protected override async Task OnInitializedAsync()
        {
            if (true)
            {
            }
            else
            {
                await Load();
            }
        }

        public async Task Load()
        {
        }
    }
}
