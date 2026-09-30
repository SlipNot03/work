using Microsoft.AspNetCore.Server.IIS.Core;

namespace ASPCoreCookies
{
    public class AppUsers
    {
        public AppUsers(string email, string password) 
        { 
            this.Email = email;
            this.Password = password;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
