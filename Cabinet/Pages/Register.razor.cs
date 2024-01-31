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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cabinet.Pages
{
    public partial class RegisterCompoenent:BasePage
    {
        public bool ImDoctor { get; set; }
        public string photo { get; set; } = string.Empty;
        public string error { get; set;} = string.Empty;

        public async Task UploadImage(InputFileChangeEventArgs file)
        {
            using (var stream = file.File.OpenReadStream())
            using (var Str = new MemoryStream())
            {
                await stream.CopyToAsync(Str);
                byte[] Bytes = Str.ToArray();
                photo = "data:image/png;base64," + Convert.ToBase64String(Bytes).ToString();
            }

        }
        protected override async Task OnInitializedAsync()
        {
            var url = Navigation.ToAbsoluteUri(Navigation.Uri);
            if (QueryHelpers.ParseQuery(url.Query).TryGetValue("error", out var error))
            {
                this.error = error;
                Notify(Radzen.NotificationSeverity.Error, "Erreur", error);
            }
        }
    }
}