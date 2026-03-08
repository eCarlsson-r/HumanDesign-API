using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Persistence.Seed;
using HumanDesign.Application.Services.Reference;
using HumanDesign.Application.Services.Reports;
using HumanDesign.Application.Services.Processing;
using HumanDesign.Application.Services.Helpers;
using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<Guid>>() // Add this line
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins!).AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<IContentResolverService, ContentResolverService>();
builder.Services.AddScoped<IAIContentGenerator, OpenAIContentGenerator>();
builder.Services.AddScoped<ReferenceDataSeeder>();
builder.Services.AddScoped<IUserHierarchyService, UserHierarchyService>();
builder.Services.AddScoped<IReferralService, ReferralService>();
builder.Services.AddScoped<IProspectService, ProspectService>(); 
builder.Services.AddScoped<FileResolver>();
builder.Services.AddScoped<IHumanDesignCalculator, HumanDesignCalculator>();
builder.Services.AddScoped<IHumanDesignReportBuilder, HumanDesignReportBuilder>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    await UserDataSeeder.SeedAsync(scope.ServiceProvider);
    await scope.ServiceProvider.GetRequiredService<ReferenceDataSeeder>().SeedAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
