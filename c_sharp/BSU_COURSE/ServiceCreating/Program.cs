var builder = WebApplication.CreateBuilder(args);

//Добавление сервиса до builder.Build()
builder.Services.AddTransient<ITimeService, ShortTimeService>();
builder.Services.AddTransient<TimeMessage>();

//Добавление сервиса через метод расширения
builder.Services.AddTimeService();

// создание объекта WebApplication
var app = builder.Build();

app.UseMiddleware<TimeMessageMiddleware>();

app.Run(async context =>
{
    //Вызов сервиса из app.Services
    //Вместо интерфейса ITimeService будет подставлен экземпляр ShortTimeService
    var timeService = app.Services.GetService<ITimeService>();
    var longTimeService = app.Services.GetService<LongTimeService>();

    //Получение сервиса из context
    var timeMessage = context.RequestServices.GetService<TimeMessage>();

    await context.Response.WriteAsync($"Time: {timeService?.GetTime()} {longTimeService?.GetTime()}");
    //await context.Response.WriteAsync($"Time: {timeMessage?.GetTime()} ");
});

app.Run();

interface ITimeService
{
    string GetTime();
}
// время в формате hh::mm
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}
// время в формате hh::mm::ss
class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}


public static class ServiceProviderExtensions
{
    public static void AddTimeService(this IServiceCollection services)
    {
        services.AddTransient<LongTimeService>();
    }
}

//Передача сервиса через конструктор класса
class TimeMessage
{
    ITimeService timeService;
    public TimeMessage(ITimeService timeService)
    {
        this.timeService = timeService;
    }
    public string GetTime() => $"Time from TimeMessage: {timeService.GetTime()}";
}

//Передача сервиса через компонент
class TimeMessageMiddleware
{
    private readonly RequestDelegate next;

    public TimeMessageMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITimeService timeService)
    {
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync($"<h1>Time from TimeMessageMiddleware: {timeService.GetTime()}</h1>");
    }
}