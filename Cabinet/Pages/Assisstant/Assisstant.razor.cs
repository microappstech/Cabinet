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
using Cabinet.Service;
using Cabinet.Pages.Assisstant;


namespace Cabinet.Pages.Assisstant
{
    public partial class AssisstantComponent:BasePage
    {
        [Inject]
        public AssisstantService assisstantService { get; set; }
        public RadzenDataGrid<Models.Assisstant> grid0;
        public IEnumerable<Models.Assisstant> assisstants { get; set; }
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
            var items = await assisstantService.GetAll();
            assisstants = items;
        }

        public async Task Ajouter()
        {
            var result = await DialogService.OpenAsync<AddAssisstant>("Ajouter une assistante", new Dictionary<string, object> { });
            await grid0.Reload();
        }
        public async Task Edit(Models.Assisstant assisstant)
        {
            var result = await DialogService.OpenAsync<EditAssisstant>("Edit une assisstante", new Dictionary<string, object> { { "Id", assisstant.Id } });
            await grid0.Reload();
        }
        public async Task Delete(EventArgs eventArgs, Models.Assisstant assisstant)
        {
            if (await Confirm("Confirmation de suppression", "Etes vous sure de vouloir supprimer cet assistant") == true)
            {
                try
                {
                    var res = await assisstantService.DeleteItem(assisstant.Id);
                    if (res)
                    {
                        Notify(Radzen.NotificationSeverity.Success, "Succès", "Suppression terminé avec succès");
                    }
                    else
                    {
                        Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                    }
                    await grid0.Reload();

                }
                catch (Exception ex)
                {
                    Notify(Radzen.NotificationSeverity.Error, "Echèc", "Suppression terminé avec erreurs");
                }
            }
        }
    }
}