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
using Cabinet.Pages.Doctors;

namespace Cabinet.Pages.Infirmier
{
    public partial class InfirmierComponent : BasePage
    {
        [Inject] InfirmierService infirmierService { get; set; }
        public IEnumerable<Models.Infirmier> infirmiers { get; set; }
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
            infirmiers = await infirmierService.GetAll();
        }

        public async Task Ajouter()
        {
            var result = await DialogService.OpenAsync<AddInfirmier>("Ajouter l'Infirmier", new Dictionary<string, object> { });
        }
        public async Task Edit(Models.Infirmier infirmier)
        {
            var result = await DialogService.OpenAsync<EditInfirmier>("Editer l'Infirmier", new Dictionary<string, object> { { "Id", infirmier.Id } });
        }
        public async Task Delete(EventArgs eventArgs, Models.Infirmier infirmier)
        {
            try
            {
                var result = await infirmierService.DeleteItem(infirmier.Id);
                if (result)
                {
                    Notify(NotificationSeverity.Success, "Succès", "Suppression térmimé avec succès");
                }
                else
                {
                    Notify(NotificationSeverity.Error, "Echèc", "something wrong");
                }
            }catch (Exception ex)
            {
                    Notify(NotificationSeverity.Error, "Echèc", "something wrong");
            }
        }
    }

}