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

namespace Cabinet.Pages.Doctors
{
    public partial class Doctor 
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if(true)
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