#pragma checksum "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d6fe4cf41eda441cc9146de43c7e8455abe890ca"
// <auto-generated/>
#pragma warning disable 1591
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/ir")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>InfraRed Remote Configuration</h1>");
#nullable restore
#line 8 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
 if (Buttons != null)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
     foreach (var button in Buttons)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "bs-callout bs-callout-info");
            __builder.OpenElement(3, "h5");
            __builder.AddMarkupContent(4, "<span class=\"oi oi-bolt\" aria-hidden=\"true\"></span> ");
            __builder.AddContent(5, 
#nullable restore
#line 13 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                                                     button.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(6, "\r\n            ");
            __builder.OpenElement(7, "p");
            __builder.OpenElement(8, "button");
            __builder.AddAttribute(9, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 15 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                    () => Edit(button.Name)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(10, "class", "btn btn-info");
            __builder.AddContent(11, "edit");
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n                ");
            __builder.OpenElement(13, "button");
            __builder.AddAttribute(14, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 16 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                    () => Delete(button.Name)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "class", "btn btn-info");
            __builder.AddContent(16, "delete");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 19 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(17);
            __builder.AddAttribute(18, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 20 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                      NewButtonModel

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(19, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(20);
                __builder2.AddAttribute(21, "placceholder", "ButtonName");
                __builder2.AddAttribute(22, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 21 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                 NewButtonModel.Value

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(23, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => NewButtonModel.Value = __value, NewButtonModel.Value))));
                __builder2.AddAttribute(24, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => NewButtonModel.Value));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(25, "\r\n        ");
                __builder2.OpenElement(26, "button");
                __builder2.AddAttribute(27, "class", "btn btn-primary");
                __builder2.AddAttribute(28, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 22 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                                  AddButton

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddMarkupContent(29, "<span class=\"oi oi-plus\" aria-hidden=\"true\"></span> Add new button");
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
#nullable restore
#line 24 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
}
else if (!watch.IsConnected && !watch.IsWaitingForConnection)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(30, "<p>Click Connect button to start</p>\r\n    ");
            __builder.OpenElement(31, "button");
            __builder.AddAttribute(32, "class", "btn btn-primary");
            __builder.AddAttribute(33, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 28 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
                                              Connect

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(34, "Connect");
            __builder.CloseElement();
#nullable restore
#line 29 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(35, "<img src=\"/img/loader-big.gif\" alt=\"loading...\" width=\"256\" height=\"256\">");
#nullable restore
#line 33 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 35 "C:\DEV\ESP32\WConfigurator\Pages\ir\Index.razor"
       
    IEnumerable<InfraButton> Buttons { get; set; }
    InfraRequestValue NewButtonModel = new InfraRequestValue("", "");

    protected override async Task OnInitializedAsync()
    {
        watch.OnUpdate = new EventCallback(this, (Func<Task>)UpdateUI);

        if (watch.IsConnected)
            await UpdateUI();
    }

    async Task UpdateUI()
    {
        if (irconfig.Buttons == null)
        {
            // Let's read current config as first step:
            await irconfig.ExecuteShowList();
        }

        Buttons = irconfig.Buttons;
    }

    private async void Connect()
    {
        await watch.Connect();
    }

    private async void AddButton()
    {
        if (NewButtonModel.Value != null)
            await irconfig.ExecuteAddNewButton(NewButtonModel.Value);
        NewButtonModel.Value = "";
    }

    void Edit(string name)
    {
        uriHelper.NavigateTo($"/ir/edit/{name}");
    }

    async Task Delete(string name)
    {
        await irconfig.ExecuteDeleteButton(name);
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
