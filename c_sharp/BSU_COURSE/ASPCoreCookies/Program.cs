using ASPCoreCookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Claims;
using System.Text;


var users = new List<AppUsers>
{
    new AppUsers("tom@test.com", "123"),
    new AppUsers("bob@test.com", "321")
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login";
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.

app.MapGet("/login", async (HttpContext context) => 
{
    context.Response.ContentType = "text/html; charset=utf-8";
    string loginForm = @"
<html>
    <head>
        <meta charset='utf-8'>
    </head>
    <body>
        <form method='post'>
            <h3>¬ход</h3>
            <p>
                <label>Email</label>
                <input type=""email"" name=""email"" />
            </p>
            <p>
                <label>ѕароль</label>
                <input type=""password"" name=""password"" />
            </p>
            <input type='submit' id='submitLogin' value='Ћогин'>
        </form>
    </body>
</html>
";
    await context.Response.WriteAsync(loginForm);
});

app.MapPost("/login", async (string? url, HttpContext context) =>
{
    var form = context.Request.Form;

    string email = form["email"];
    string password = form["password"];

    AppUsers? user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

    if (user == null)
        return Results.Unauthorized();

    var claim = new List<Claim> { new Claim(ClaimTypes.Name, email) };

    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, "Cookies");

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    return Results.Redirect(url ?? "/");
});

app.Map("/", [Authorize]() => $"Hello");

app.MapGet("/logout", async (HttpContext context) => {
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.Run();





