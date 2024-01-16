using Azure.Identity;
using TutorialAzureKeyVaultCreateAndUseConcise.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ==============================
// Start code to add for tutorial
// ==============================
// Initialize ConfigurationManager with the standard providers (appsettings.json, environment variables, etc)
var configuration = builder.Configuration;

// LIMITING TO PRODUCTION IS NOT MANDATORY; THE CODE WILL ALSO WORK IN DEVELOPMENT (Visual Studio).
if (1==1) // (builder.Environment.IsProduction())
{
    configuration.AddAzureKeyVault(
            new Uri("https://kentestkeyvault.vault.azure.net/"), // <--- this literal should be in appsettings.json!
            new DefaultAzureCredential());                 // Allows this app to run on VisualStudio (Development) and Production!
}
// ==============================
// End code to add for tutorial
// ==============================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
