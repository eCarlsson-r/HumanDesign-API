using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Application.Services.Reference;
using HumanDesign.Application.Services.Reports;
using HumanDesign.Application.Services.Diagram;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();
builder.Services.AddScoped<IHumanDesignReportBuilder, HumanDesignReportBuilder>();
builder.Services.AddScoped<DiagramModelBuilder>();
builder.Services.AddScoped<IDiagramRenderer, SvgDiagramRenderer>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var refSvc = scope.ServiceProvider.GetRequiredService<IReferenceDataService>();
await refSvc.GetBundleAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();
