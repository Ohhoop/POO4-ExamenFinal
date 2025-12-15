using CreditImpot.API.Data;
using CreditImpot.API.Interfaces;
using CreditImpot.API.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CreditImpotContext>(options =>
                      options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICalculCredit, CalculCredit>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "DataProtection-Keys")))
    .SetApplicationName("CreditImpot");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API CreditImpot",
        Version = "v1",
        Description = "API permetent de faire des demande de credit",
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org")
        },
        Contact = new OpenApiContact
        {
            Name = "Hinault Donfack",
            Email = "hinault.donfack@cegeplimoilou.ca",
            Url = new Uri("https://modern-ti.com/")
        }
    });
    //Activation du support des commentaires XML dans Swagger UI

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CreditImpotContext>();
    context.Database.EnsureCreated();
    DBInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
