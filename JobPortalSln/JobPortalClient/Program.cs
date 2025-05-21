using JobPortalClient;
using JobPortalClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using JobPortalClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddScoped(sp =>
{
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var handler = new AuthHeaderHandler(localStorage)
    {
        InnerHandler = new HttpClientHandler()
    };
    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7061/") };
});

await builder.Build().RunAsync();
