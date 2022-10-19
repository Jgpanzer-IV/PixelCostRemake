using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PixelCost.Client.Web;
using PixelCost.Client.Web.Services.Implements;
using PixelCost.Client.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// This method is nessesary to set to be false to retrieve id resource from identity server
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = "cookies";
    option.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookies", option => {
        option.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix;
        option.AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath;
        option.ExpireTimeSpan = TimeSpan.FromDays(12);
    })
    .AddOpenIdConnect("oidc", config => {

        // Set the remote authentication server.
        config.Authority = builder.Configuration.GetSection("UriServers")["identityServer"]; // Specify authority to make sure that the token will come from the authentication server
        config.AccessDeniedPath = "/Auth/AccessDenied"; // The route template used when cancel signing-in returned from authentication server.
        config.SignedOutRedirectUri = "/Index"; // The route template used when signout from authentication server.

        // Set Client Credencial to be validate at the authentication server.
        config.ClientId = builder.Configuration["Credencial:ClientID"];
        config.ClientSecret = builder.Configuration["Credencial:ClientSecret"];
        config.ResponseType = builder.Configuration["Credencial:ResponseType"];

        config.Scope.Clear();

        // Add Requesting for identity token Identity scope
        /*      foreach (var i in builder.Configuration.GetSection("IdentityScope").AsEnumerable())
              {
                  if (i.Value != null)
                      config.Scope.Add(i.Value);
              }*/
        config.Scope.Add("openid");
        config.Scope.Add("profile");
        // Add Requesting for access token
        foreach (var i in builder.Configuration.GetSection("ApiScope").AsEnumerable())
        {
            if (i.Value != null)
                config.Scope.Add(i.Value);
        }

        

        //config.Scope.Add("role");
        config.GetClaimsFromUserInfoEndpoint = true;

        //config.ClaimActions.MapUniqueJsonKey("role", "role");

        config.SaveTokens = true;
    });

builder.Services.AddHttpClient(Constant.walletUserApi, config => {
    config.BaseAddress = new Uri(builder.Configuration["UriServers:walletAPI"]);
    config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<ICommunicationServices, CommunicationServices>();















// -------------------------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
