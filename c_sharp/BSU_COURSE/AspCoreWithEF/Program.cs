using AspCoreWithEF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppUserContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.MapGet("api/users", (AppUserContext db) => 
{
    //db.Users.ToList();
    return db.Users.FromSqlRaw("select * from Users").ToList();
});

app.MapGet("api/users/{id:int}", async (int id, AppUserContext db) => 
{
    User? user = await db.Users.SingleOrDefaultAsync(u=> u.Id == id);
    if (user == null)
    {
        return Results.NotFound(new { message = "user not found"});
    }
    return Results.Json(user);
});

app.MapDelete("api/users/{id:int}", async (int id, AppUserContext db) =>
{
    User? user = await db.Users.SingleOrDefaultAsync(u => u.Id == id);
    if (user == null)
    {
        return Results.NotFound(new { message = "user not found" });
    }
    db.Users.Remove(user); //delete from
    await db.SaveChangesAsync(); //commit
    return Results.Json(user);
});

app.MapPost("api/users", async (User user, AppUserContext db) =>
{
    db.Users.AddAsync(user); //insert
    await db.SaveChangesAsync(); //commit
    return Results.Json(user);
});

app.MapPut("api/users", async (User userData, AppUserContext db) =>
{
    User? user = await db.Users.SingleOrDefaultAsync(u => u.Id == userData.Id);
    if (user == null)
    {
        return Results.NotFound(new { message = "user not found" });
    }
    //update
    user.Name = userData.Name;
    user.Age = userData.Age;
    await db.SaveChangesAsync(); //commit
    return Results.Json(user);
});




app.Run();
