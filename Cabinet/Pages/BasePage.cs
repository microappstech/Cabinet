using Microsoft.AspNetCore.Components;
using Cabinet.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Extensions;

namespace Cabinet.Pages
{
    public partial class BasePage : ComponentBase
    {
        [Inject] protected NavigationManager Navigation { get; set; }
        [Inject] protected SecurityService Security { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] NotificationService notificationService { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        public void Notify(NotificationSeverity r, string Title, string Message)
        {
            notificationService.Notify(r, Message, Title, duration: 4000);
        }

        public void Confrm(NotificationSeverity r, string Title, string Message)
        {
            DialogService.Confirm(Message, Title,new ConfirmOptions { CancelButtonText="Annuler", OkButtonText = "Confirmer" }) ;
        }
    }
}
