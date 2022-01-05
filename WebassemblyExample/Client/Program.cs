using Controls.Sidebars.States;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebassemblyExample.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<SidebarState, SidebarState>();
builder.Services.AddSingleton<NavItemState, NavItemState>();
builder.Services.AddSingleton<NavHeaderState, NavHeaderState>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var host = builder.Build();

var sidebarState = host.Services.GetRequiredService<SidebarState>();
sidebarState.SetItems(ExampleSidebar.Items);
sidebarState.Title = ExampleSidebar.Title;

await host.RunAsync();