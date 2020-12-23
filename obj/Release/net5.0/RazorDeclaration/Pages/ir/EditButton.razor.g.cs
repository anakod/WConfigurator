// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace WConfigurator.Pages.ir
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using WConfigurator;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using WConfigurator.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using WConfigurator.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using WConfigurator.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\DEV\ESP32\WConfigurator\_Imports.razor"
using Blazm.Bluetooth;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/ir/edit/{Name}")]
    public partial class EditButton : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 54 "C:\DEV\ESP32\WConfigurator\Pages\ir\EditButton.razor"
       
    [Parameter]
    public string Name { get; set; }
    protected ResponseButtonEdit Model { get; set; }
    bool isSaving;

    protected override async Task OnInitializedAsync()
    {
        if (!watch.IsConnected)
        {
            uriHelper.NavigateTo($"/ir");
            return;
        }

        watch.OnUpdate = new EventCallback(this, (Action)Updated);

        // Let's read current button params as first step:
        await irconfig.ExecuteShowEditButton(Name);
    }

    void Updated()
    {
        if (isSaving && irconfig.CurrentButton != null)
        {
            // New values successful saved
            isSaving = false;
            uriHelper.NavigateTo($"/ir");
            return;
        }

        Model = irconfig.CurrentButton;
    }

    private async void OnSubmit()
    {
        isSaving = true;
        await irconfig.ExecuteSaveEditButton(Model);
    }

    void OnCancel()
    {
        uriHelper.NavigateTo($"/ir");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager uriHelper { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private InfraRemote irconfig { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private WatchService watch { get; set; }
    }
}
#pragma warning restore 1591
