using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Practice.Web.Mvc.Hosting;
using Practice.Web.Core;

var builder = WebApplication.CreateBuilder(args);

builder.UseCore();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;

        var validIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer");
        var signingKey = builder.Configuration.GetValue<string>("JwtSettings:SigningKey");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            ValidateIssuer = true,
            ValidIssuer = validIssuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
                {
                    return Task.CompletedTask;
                }
                var token = context.HttpContext.Request.Cookies[Practice.Web.Mvc.Constants.HardCode.Cookie.JwtToken];
                if (!string.IsNullOrEmpty(token))
                {
                    context.HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHostedService<PrePreparationHostedService>();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.Name = ".Practice.Web.Session";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();