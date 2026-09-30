using ASPCoreJWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


var users = new List<AppUsers>
{
    new AppUsers("tom@test.com", "123"),
    new AppUsers("bob@test.com", "321")
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//JWT - JSON Web Token: header, payload, signature
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true, //проверка издателя
        ValidIssuer = AuthOptions.ISSUER, //издатель
        ValidAudience = AuthOptions.AUDIENCE, //проверка клиента
        ValidateLifetime = true, //проверка времени токена
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(), //формирование ключа
        ValidateIssuerSigningKey = true, //проверка ключа
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/login", (AppUsers appUser) =>
{
    AppUsers? user = users.FirstOrDefault(u => u.Email == appUser.Email && u.Password == appUser.Password);

    if (user is null)
        return Results.Unauthorized(); 
    
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        accessToken = jwtToken,
        username = appUser.Email
    };

    return Results.Json(response);
});

app.MapPost("login", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    return new JwtSecurityTokenHandler().WriteToken(jwt);
});

app.MapGet("/hello", [Authorize] () => new { message = "Hello!" });

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; //строка издателя
    public const string AUDIENCE = "AuthClient"; //строка для клиента
    private const string KEY = "secretKey_dfnkjdfmksdtkm45dfsdfsdf155dfdf"; //ключ
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}

