using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Qi_practice_authentication.Data;
using Qi_practice_authentication.Entities;
using Qi_practice_authentication.Helpers.JwtMiddleWear;
using Qi_practice_authentication.Services;
using Qi_practice_authentication.Services.OurHeros;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    swagger =>
    {
        swagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "vi",
            Title = "Jwt Token Authentication API",
            Description = ".NET 8 Web API"
        });
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. "
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    }
    );

builder.Services.AddSingleton<IOurHeroService, OurHeroService>();
builder.Services.AddDbContext<OurHeroDbContext>(db => db.UseSqlite(builder.Configuration.GetConnectionString("OurHeroConnectionString")), ServiceLifetime.Singleton);
// builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.Configure<Appsettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
var app = builder.Build();

 if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<JwtMiddleWear>();
app.UseAuthorization();

app.Run();

