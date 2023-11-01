using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MyFreelas.Infra.IoC;

public static class DependencyInjectionSwagger
{

    public static void AddInfrastructureSwagger(this IServiceCollection services)
    {
        AddSwagger(services); 
        AddVersioning(services);
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "MyFrellasAPI",
            Version = "v1",
            Description = "Gerenciador de projetos freelas",
            Contact = new OpenApiContact
            {
                Name = "Matheus Siqueira",
                Email = "matheussiqueira.work@gmail.com",
                Url = new Uri("https://github.com/matheuz-siqueira")
            }
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Header de autorização JWT usando o esquema Bearer.\r\n\r\nInforme"
            + "'Bearer'[espaço] e o seu token.\r\n\r\nExemplo: Bearer NDczMjVjMDYtMWM5Yy00MDQ0LWE"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        var xmlFile = "MyFreelas.WebUI.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);  
        });
    }

    private static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(c =>
        {
            c.GroupNameFormat = "'v'VVV";
            c.SubstituteApiVersionInUrl = true;
        });
    }
}


