var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//REST : GET POST PUT DELETE

List<Person> users = new List<Person> {
    new Person{ Id = Guid.NewGuid().ToString(), Name="Test1", Age=21},
    new Person{ Id = Guid.NewGuid().ToString(), Name="Test2", Age=22},
    new Person{ Id = Guid.NewGuid().ToString(), Name="Test2", Age=23},
};

app.Run(async (context) => {
    var response = context.Response;
    var request = context.Request;
    var path = request.Path;

    if (path == "/api/user" && request.Method == "GET")
    {
        await GetAllUsers(response);
    } 
    else if (path == "/api/users" && request.Method== "POST")
    {
        await CreateUser(response, request);
    }
    else if (path=="/api/users" && request.Method=="PUT")
    {
        await UpdateUser(response, request);
    }
    else if (path=="/api/users" && request.Method=="DELETE")
    {
        var id = path.Value.Split("/")[3];
        await DeleteUser(id, response);
    } else if (path=="api/users" && request.Method=="GET")
    {
        var id = path.Value.Split("/")[3];
        await GetUser(id, response);
    }
});

async Task GetAllUsers(HttpResponse response)
{
    await response.WriteAsJsonAsync(users);
}

async Task GetUser(string? Id, HttpResponse response)
{
    Person? user = users.FirstOrDefault(users => users.Id == Id);
    if (user != null)
    {
        await response.WriteAsJsonAsync(user);
    } else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден"});
    }
}

async Task DeleteUser(string? Id, HttpResponse response)
{
    Person? user = users.FirstOrDefault(users => users.Id == Id);
    if (user != null)
    {
        users.Remove(user);
        await response.WriteAsJsonAsync(user);
    } else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
    }
}

async Task CreateUser(HttpResponse response, HttpRequest request)
{
    try
    {
        var user = await request.ReadFromJsonAsync<Person>();
        if (user != null)
        {
            user.Id = Guid.NewGuid().ToString();
            users.Add(user);
            response.WriteAsJsonAsync(user);
        }
        else
        {
            throw new Exception("Ошибка при создании пользователя");
        }
    } catch (Exception ex) { 
        response.StatusCode = 500;
        await response.WriteAsJsonAsync(new { message = ex.Message });
    }
}

async Task UpdateUser(HttpResponse response, HttpRequest request)
{
    try
    {
        var userData = await request.ReadFromJsonAsync<Person>();
        if (userData != null) {
            var user = users.FirstOrDefault(u => u.Id == userData.Id);
            if (user != null)
            {
                user.Age = userData.Age;
                user.Name = userData.Name;
                await response.WriteAsJsonAsync(user);
            }
            else
            {
                throw new Exception("Ошибка при обновлении пользователя");
            }

        }
    }
    catch (Exception ex)
    {
        response.StatusCode = 500;
        await response.WriteAsJsonAsync(new { message = ex.Message });
    }
}


public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
