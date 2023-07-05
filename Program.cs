using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using myfreelas.Data;
using myfreelas.Dtos.User;
using myfreelas.Mapper;
using myfreelas.Models;
using myfreelas.Repositories.User;
using myfreelas.Services.User;
using myfreelas.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();


builder.Services.AddDbContext<Context>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))      
    )
);

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg => {
    cfg.AddProfile(new MappingProfile());
}).CreateMapper());



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

app.UseAuthorization();

app.MapControllers();

app.Run();
