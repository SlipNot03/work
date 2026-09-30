using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


string date = "";

//
app.Use(async (context, next) =>
{
    date = DateTime.Now.ToShortDateString();
    await next.Invoke(); //вызов из app.Run

    //После работы app.Run возвращение в эту точку
    Console.WriteLine($"Current date {date}");
});

//Использование метода как параметра (второй компонент)
app.Use(getDate); 

async Task getDate(HttpContext context, Func<Task> next)
{
    string path = context.Request.Path.Value?.ToLower();
    if (path == "/date")
    {
        await context.Response.WriteAsync("Path = date: " + date);
    } else
    {
        await next.Invoke();
        Console.WriteLine("After next.Invoke in getDate");
    }

}

//конечный middleware (без вызова next.Invoke)
app.Run(async(context) => await context.Response.WriteAsync($"Date {date}"));

app.Run();
