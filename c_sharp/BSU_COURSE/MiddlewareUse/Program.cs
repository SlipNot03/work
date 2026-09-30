using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


string date = "";

//Компонент для условного конвейера (схожий метод MapWhen)
app.UseWhen(
    context => context.Request.Path == "/time",
    //Обработку можно сделать через метод
    HandleTimeRequest
//либо через делегат

//appBuilder =>
//{
//    appBuilder.Use(async (context, next) =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        Console.WriteLine($"Time {time}");
//        await next();
//    });

//    appBuilder.Run(async context =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        await context.Response.WriteAsync($"time: {time}");
//    });
//}
);

void HandleTimeRequest(IApplicationBuilder appBuilder)
{
    appBuilder.Use(async (context, next) =>
    {
        var time = DateTime.Now.ToShortTimeString();
        Console.WriteLine($"Time {time}");
        await next();
    });
}

//конечный middleware (без вызова next.Invoke)
app.Run(async (context) => await context.Response.WriteAsync($"app.Run: Date {date}"));

app.Run();
