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
using Cabinet.Service;
using Cabinet.Models;

namespace Cabinet.Pages.Infirmier
{
    public partial class AddInfirmierComponent : BasePage
    {
        [Inject] InfirmierService infirmierService {  get; set; }
        public Models.Infirmier infirmier{ get; set; }
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
            infirmier = new Models.Infirmier(){};
        }


        public async Task Submit(Models.Infirmier infirmier)
        {
            try
            {
                User us = new User()
                {
                    RoleNames = new string[]
                    {
                        "infirmier".ToUpper()
                    },
                    FullName = infirmier.FullName,
                    Photo = infirmier.Photo,
                    Password = infirmier.Password,
                };
                var res = await Security.CreateUser(us);
                infirmier.UserId = res.Id;
                infirmier.UserName = res.UserName;
                var result = await infirmierService.CreateItem(infirmier);
                await InvokeAsync(StateHasChanged);
                DialogService.Close();
                Notify(NotificationSeverity.Success, "Création terminé avec succès", "Succès");
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