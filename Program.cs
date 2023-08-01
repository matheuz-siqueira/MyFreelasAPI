using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using myfreelas.Data;
using myfreelas.Mapper;
using myfreelas.Repositories.User;
using myfreelas.Services.User;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using myfreelas.Authentication;
using myfreelas.Dtos.User;
using myfreelas.Validators;
using Microsoft.AspNetCore.Mvc;
using myfreelas.Repositories.Customer;
using myfreelas.Services.Customer;
using myfreelas.Dtos;
using HashidsNet;
using myfreelas.Repositories.Freela;
using myfreelas.Services.Freela;
using myfreelas.Dtos.Freela;
using myfreelas.Services.Dashboard;
using myfreelas.Repositories.Installment;
using myfreelas.Repositories.Dashboard;
using myfreelas.Dtos.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IValidator<RequestAuthenticationJson>, AuthenticationValidator>();
builder.Services.AddScoped<IValidator<RequestUpdatePasswordJson>, UpdatePasswordValidator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IValidator<RequestCustomerJson>, RegisterCustomerValidator>();
builder.Services.AddScoped<IFreelaRepository, FreelaRepository>();
builder.Services.AddScoped<IFreelaService, FreelaService>();
builder.Services.AddScoped<IValidator<RequestRegisterFreelaJson>, RegisterFreelaValidator>();
builder.Services.AddScoped<IValidator<RequestUpdateFreelaJson>, UpdateFreelaValidator>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IInstallmentRepository, InstallmentRepository>();
builder.Services.AddScoped<IValidator<RequestGetMonthlyBillingJson>, GetMonthlyBillingValidator>();


builder.Services.AddDbContext<Context>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);


builder.Services.AddScoped<IHashids>(_ =>
    new Hashids(builder.Configuration.GetValue<string>("HashIds:Salt"), 3)
);

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile(provider.GetService<IHashids>()));
}).CreateMapper());


//Configurações para usar Autenticação com JWT
var JWTKey = Encoding.ASCII.GetBytes(builder.Configuration["JWTKey"]);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(JWTKey),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(c =>
{
    c.GroupNameFormat = "'v'VVV";
    c.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
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
            Url = new Uri("https://www.linkedin.com/in/matheussiqueira-me/")
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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
