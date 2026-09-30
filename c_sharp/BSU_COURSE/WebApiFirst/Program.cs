using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

List<Person> users = new List<Person> {
    new Person() { Id = 1, Name="Tom", Age=21},
    new Person() { Id = 2, Name="Bob", Age=22},
    new Person() { Id = 3, Name="Sam", Age=23}
};

//получить всех
app.MapGet("/api/users", ()=> users);

//получить по id
app.MapGet("/api/users/{id}", (int? id) =>
{
    Person? person = users.FirstOrDefault(u => u.Id == id);
    if (person == null)
    {
        return Results.NotFound(new {message = "not found user"});
    }
    return Results.Json(person);
});
//удаление
app.MapDelete("/api/users/{id}", (int? id) =>
{
    Person? person = users.FirstOrDefault(u => u.Id == id);
    if (person == null)
    {
        return Results.NotFound(new { message = "not found user" });
    };
    users.Remove(person);
    return Results.Json(person);
});
//добавление
app.MapPost("/api/users", (Person person) => { 
    person.Id = 1;
    users.Add(person);
    return Results.Json(person);
});
//изменение
app.MapPut("/api/users", (Person user) => {
    Person? person = users.FirstOrDefault(u => u.Id == user.Id);
    if (person == null)
    {
        return Results.NotFound(new { message = "not found user" });
    };
    person.Name = user.Name;
    person.Age = user.Age;
    return Results.Json(person);
});


app.Run();

public class Person
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}