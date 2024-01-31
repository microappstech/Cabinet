
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cabinet.Pages
{
    public partial class LoginComponent :BasePage
    {
        [Inject] NavigationManager navigation { get; set; }
        private string password { get; set; }
        public string redirectUrl { get; set; }
        protected bool ShowPassword = false;
        public string error { get; set; }

        protected void handleShowPassword()
        {
            ShowPassword = !ShowPassword;
        }
        protected override async Task OnInitializedAsync()
        {
            var url = navigation.ToAbsoluteUri(navigation.Uri);
            if (QueryHelpers.ParseQuery(url.Query).TryGetValue("error",out var error))
            {
                this.error = error;
                Notify(Radzen.NotificationSeverity.Error, "Erreur", error);
            }
        }
    }
}