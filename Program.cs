using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using myfreelas.Data;
using myfreelas.Dtos.User;
using myfreelas.Mapper;
using myfreelas.Repositories.User;
using myfreelas.Services.User;
using myfreelas.Validators;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<Context>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))      
    )
);

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg => {
    cfg.AddProfile(new MappingProfile());
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
