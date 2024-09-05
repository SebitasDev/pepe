using System.Net;
using System.Text;
using backend.Services.Interface;
using backend.Services.Repository;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Services.Interface;
using RiwiTalent.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//DotNetEnv
Env.Load();

//Service to MongoDb
builder.Services.AddSingleton<MongoDbContext>();

//Services to Interface and Repository
builder.Services.AddScoped<ICoderRepository, CoderRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

//Configuration JWT with environment variables
builder.Services.AddAuthentication(option => {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(configure => {
            configure.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("Key"),
                ValidAudience = Environment.GetEnvironmentVariable("Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(Environment.GetEnvironmentVariable("Issuer")))
            };
            /* Control de errores del token */
            configure.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    if(context.Exception is SecurityTokenExpiredException)
                    {
                        Console.WriteLine("Token expirado, porfavor genere uno nuevo.");
                    }
                    else
                    {
                        Console.WriteLine("Usuario no autorizado.");
                    }
                    return Task.CompletedTask;
                }
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

//Controllers
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
