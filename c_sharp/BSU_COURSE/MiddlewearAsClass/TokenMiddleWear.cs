namespace MiddlewearAsClass
{
    public class TokenMiddleWear
    {
        private readonly RequestDelegate next;
        private string pattern;

        public TokenMiddleWear(RequestDelegate next, string pattern)
        //public TokenMiddleWear(RequestDelegate next)
        {
            this.next = next;
            this.pattern = pattern;
            //this.pattern = "123";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];

            if (token != this.pattern)
            {
                context.Response.Headers.ContentType = "text/html; charset=utf-8";
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Неверный токен");
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
