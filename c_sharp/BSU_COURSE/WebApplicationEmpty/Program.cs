using System.IO;
using System.Text;

//WebApplication применяется для управления обработкой запроса, установки маршрутов, получения сервисов и т.д. 
var builder = WebApplication.CreateBuilder(args);

/*
    Установка конфигурации приложения
    Добавление сервисов
    Настройка логгирования в приложении
    Установка окружения приложения
    Конфигурация объектов IHostBuilder и IWebHostBuilder, которые применяются для создания хоста приложения
    */
var app = builder.Build();

//middlewear - компонент конвейра для обработки запросов
// request  -> component1 -> component2 -> .. -> componentN
// response <- component1 <- component2 <- .. <- componentN
//WelcomePage - пример middlewear для отрисовки стартовой страницы
//app.UseWelcomePage();


//Run - запускает терминальный компонент, после которого не будет дальнейшей обработки
//IApplicationBuilder.Run(RequestDelegate handler)
//Вид делегата public delegate Task RequestDelegate(HttpContext context);
//int x = 2;
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync($"Result: {x *= 2}");
//});

#region Пример установки заголовка в ответе
//app.Run(async (context) =>
//{
//    context.Response.Headers.ContentType = "text/html; charset=utf-8";
//    context.Response.Headers.ContentLanguage = "ru-RU";
//    context.Response.StatusCode = 200;

//    await context.Response.WriteAsync("Привет!!!");
//});
#endregion

#region Отправка html
//app.Run(async (context) =>
//{
//    var response = context.Response;
//    response.ContentType = "text/html; charset=utf-8";
//    await response.WriteAsync("<h2>Hello</h2><h3>Welcome to ASP.NET Core</h3>");
//});
#endregion

#region Заголовки из запроса
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    var stringBuilder = new System.Text.StringBuilder("<table>");

//    foreach (var header in context.Request.Headers)
//    {
//        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
//    }
//    stringBuilder.Append("</table>");
//    await context.Response.WriteAsync(stringBuilder.ToString());
//});
#endregion

#region Параметры запроса
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    var stringBuilder = new System.Text.StringBuilder("<table>");
//    stringBuilder.Append($"Путь: {context.Request.Path}");
//    stringBuilder.Append($"QueryString: {context.Request.QueryString}");
//    stringBuilder.Append("<table>");
//    foreach (var param in context.Request.Query)
//    {
//        stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
//    }
//    stringBuilder.Append("</table>");

//    await context.Response.WriteAsync(stringBuilder.ToString());
//});
#endregion

#region Отправка файла
//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";

//    //Отправка одного файла
//    //await context.Response.SendFileAsync("html/index.html");

//    var path = context.Request.Path;
//    var fullPath = $"html/{path}";

//    if (File.Exists(fullPath))
//    {
//        await context.Response.SendFileAsync(fullPath);
//    }
//    else if (context.Request.Path == "/postuser" && context.Request.Method == HttpMethod.Post.ToString())
//    {
//        var form = context.Request.Form;
//        await context.Response.WriteAsync($"<div><p>Имя {form["name"]}</p><p>Возраст {form["age"]} </p></div>");
//    }
//    else if (context.Request.Path == "/contacts")
//    {
//        //Перенаправление запроса
//        context.Response.Redirect("https://ya.ru");
//    }
//    else
//    {
//        /*        context.Response.StatusCode = 404;
//                await context.Response.WriteAsync("Файл не найден");*/

//        await context.Response.SendFileAsync("html/index.html");

//    }
//});
#endregion



//Окончание обработки запроса
app.Run();

public record Person(string Name, int Age);
