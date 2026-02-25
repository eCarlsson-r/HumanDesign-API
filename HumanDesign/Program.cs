using SharpAstrology.Ephemerides;
using SharpAstrology.Interfaces;
using SharpAstrology.Enums;
using SharpAstrology.DataModels;
using SharpAstrology.HumanDesign;
using HumanDesignApi;
using HumanDesignApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Register SharpAstrology services
builder.Services.AddSingleton<SwissEphemeridesService>(new SwissEphemeridesService(ephType: EphType.Moshier));
builder.Services.AddScoped<IEphemerides>(sp => sp.GetRequiredService<SwissEphemeridesService>().CreateContext());

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HumanDesignDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<HumanDesignApi.Services.HumanDesignService>();
builder.Services.AddSingleton<HumanDesignApi.Services.SvgService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.MapGet("/generate_human_design_chart", (string dateTimeString, string timezone, IEphemerides eph) =>
{
    string dateString;
    string timeString;
    if (dateTimeString.Contains("T")) {
        dateString = dateTimeString.Split("T")[0];
        timeString = dateTimeString.Split("T")[1];
    } else if (dateTimeString.Contains(" ")) {
        dateString = dateTimeString.Split(" ")[0];
        timeString = dateTimeString.Split(" ")[1];
    } else {
        dateString = dateTimeString;
        timeString = "12:36";
    }
    // Calculate the chart
    var chart = new HumanDesign(eph, dateString, timeString, timezone);

    return chart.ToJSONString();    
})
.WithName("GetHumanDesignChart");

app.MapGet("/generate_hd", (string dob, string tob, string timezone, string? name, HumanDesignApi.Services.HumanDesignService hdService, IEphemerides eph) =>
{
    var chart = new HumanDesign(eph, dob, tob, timezone);
    var legacyResponse = hdService.MapToLegacy(chart, name ?? "Unnamed", dob, tob);
    return Results.Ok(legacyResponse);
})
.WithName("GenerateHD");
 
app.MapGet("/generate_hd_svg", (string dob, string tob, string timezone, string? name, HumanDesignApi.Services.HumanDesignService hdService, HumanDesignApi.Services.SvgService svgService, IEphemerides eph) =>
{
    var chart = new HumanDesign(eph, dob, tob, timezone);
    var legacyResponse = hdService.MapToLegacy(chart, name ?? "Unnamed", dob, tob);
    var svg = svgService.GenerateDesignImage(legacyResponse);
    return Results.Content(svg, "image/svg+xml");
})
.WithName("GenerateHDSvg");

app.Run();
