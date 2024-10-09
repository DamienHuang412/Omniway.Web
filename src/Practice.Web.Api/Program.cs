using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Practice.Web.Api;
using Practice.Web.Api.Hosting;
using Practice.Web.Api.Interfaces;
using Practice.Web.Api.Managers;
using Practice.Web.Core;

var builder = WebApplication.CreateBuilder(args);

builder.UseCore();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAllowlistManager, AllowlistManager>();

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
    });
builder.Services.AddHealthChecks()
    .AddCheck<SimpleHealthCheck>("Simple");
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", 
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization"
        });
    
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

builder.Services.AddHostedService<PrePreparationHostedService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.Name = ".Practice.Web.Session";
    options.Cookie.HttpOnly = true;
});
const string policyName = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policyBuilder =>
        policyBuilder
            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapSwagger()
    .RequireAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.UseCors(policyName);

app.Run();