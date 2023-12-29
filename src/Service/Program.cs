using Application;
using Common.Config;
using Domain;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DomainModule.RegisterServices(builder.Services);
ApplicationModule.Registration(builder.Services);
InfrastructureModule.RegisterServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PIMS",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

builder.Services.AddHttpClient();

builder.Services.Configure<DataBaseSettings>(builder.Configuration.AddJsonFile("appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json").Build().GetSection("DataBaseSettings"));
builder.Services.Configure<BlobStorage>(builder.Configuration.AddJsonFile("appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json").Build().GetSection("BlobStorage"));
builder.Configuration.GetSection("CommonSettings").Bind(AppSettings.CommonSettings);//to allow settings in static file
 
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
