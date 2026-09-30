var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Создание ветки конвейера
app.Map(
    "/time", 
    appBuilder => 
    { 
        var time = DateTime.Now.ToShortTimeString();
        appBuilder.Use(async (context, next) =>
        {
            Console.WriteLine($"Time {time}");
            await next();
        });

        appBuilder.Run(async context => await context.Response.WriteAsync($"Time {time}"));

    });

app.Map(
    "/user",
    appBuilder =>
    {
        var user = "user01";
        appBuilder.Use(async (context, next) =>
        {
            Console.WriteLine($"User {user}");
            await next();
        });

        appBuilder.Run(async context => await context.Response.WriteAsync($"User {user}"));

    });

app.Map(
    "/home",
    appBuilder =>
    {
        appBuilder.Map("/about", About);
        appBuilder.Run(async context => await context.Response.WriteAsync($"Home"));

    });

void About(IApplicationBuilder appBuilder)
{
    appBuilder.Run(async context => await context.Response.WriteAsync("About"));
}

app.Run(async (context) => await context.Response.WriteAsync("Hello"));

app.Run();
