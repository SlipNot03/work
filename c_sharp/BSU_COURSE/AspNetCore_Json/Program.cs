using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Обработка json
//app.Run(async (context) =>
//{
//    //Передача данных клиенту
//    Person testPerson = new("TestPerson", 22);
//    //Передача данных в виде JSON
//    //await context.Response.WriteAsJsonAsync(testPerson);

//    //JSON
//    var response = context.Response;
//    var request = context.Request;

//    //Проверяем путь и содержимое запроса
//    if (request.Path == "/api/user" && request.HasJsonContentType())
//    {
//        var message = "";
//        try
//        {
//            var person = await request.ReadFromJsonAsync<Person>();
//            if (person != null)
//            {
//                message = $"Name: {person.Name}, Age: {person.Age}";
//            }
//        }
//        catch { }
//        await response.WriteAsJsonAsync(new { text = message });
//    }
//    else
//    {
//        response.ContentType = "text/html; charset=utf-8";
//        await response.SendFileAsync("html/index.html");
//    }
//});
#endregion

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    if (request.Path == "/api/user")
    {
        var responseText = "Неверные данные";

        if (request.HasJsonContentType())
        {
            //параметры десериализации
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new PersonConverter());
            var person = await request.ReadFromJsonAsync<Person>(jsonOptions);
        }
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();

public record Person(string Name, int Age);

public class PersonConverter: JsonConverter<Person>
{
    public override Person? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var personName = "Undefined";
        var personAge = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                switch (propertyName?.ToLower())
                {
                    case "age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();
                        break;
                    case "age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // пытаемся конвертировать строку в число
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "name":    // если свойство Name/name
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;

                }
            }
        }
        return new Person(personName, personAge);
    }

    //сериализация в Json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);
        writer.WriteEndObject();
    }
}