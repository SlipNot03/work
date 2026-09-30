using Microsoft.AspNetCore.Builder;

namespace MiddlewearAsClass
{
    public static class TokenExtension
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
        { 
            return builder.UseMiddleware<TokenMiddleWear>(pattern);
        }
    }
}
