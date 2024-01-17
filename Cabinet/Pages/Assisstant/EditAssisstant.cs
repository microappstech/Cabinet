using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Cabinet;
using Cabinet.Shared;
using Radzen.Blazor;
using Radzen;
using Cabinet.Models;
using Cabinet.Service;

namespace Cabinet.Pages.Assisstant
{
    public partial class EditAssisstantComponent:BasePage
    {
        [Parameter] public dynamic Id { get; set; }
        [Inject] AssisstantService assisstantService { get; set; }
        public Models.Assisstant assisstant { get; set; }
        
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
            assisstant = await assisstantService.GetById(Convert.ToInt32(Id));
        }
        public async Task Submit(Models.Assisstant assisstant)
        {
            try
            {
                var result = await assisstantService.UpdateItem(assisstant);
                await InvokeAsync(StateHasChanged);
                DialogService.Close();

                Notify(NotificationSeverity.Success, "L'édition termné avec succès", "Succès");
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