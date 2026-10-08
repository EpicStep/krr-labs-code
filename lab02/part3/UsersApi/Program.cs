using Microsoft.EntityFrameworkCore;
using UsersApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Users") ?? "Data Source=users.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<UsersContext>().Database.EnsureCreated();

app.MapPost("/user", async (UserRequest request, UsersContext db) =>
{
    var error = request.Validate();
    if (error != null)
        return Results.BadRequest(new MessageResponse(error));

    if (await db.Users.AnyAsync(u => u.Login == request.Login))
        return Results.Conflict(new MessageResponse($"Логин {request.Login} уже занят"));

    var user = new User { Login = request.Login!, PassHash = request.PassHash!.ToLowerInvariant() };
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", UserResponse.From(user));
});

app.MapGet("/user/{id:int}", async (int id, UsersContext db) =>
{
    var user = await db.Users.FindAsync(id);
    return user == null
        ? Results.NotFound(new MessageResponse($"Пользователь {id} не найден"))
        : Results.Ok(UserResponse.From(user));
});

app.MapPut("/user/{id:int}", async (int id, UserRequest request, UsersContext db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user == null)
        return Results.NotFound(new MessageResponse($"Пользователь {id} не найден"));

    var error = request.Validate();
    if (error != null)
        return Results.BadRequest(new MessageResponse(error));

    if (await db.Users.AnyAsync(u => u.Login == request.Login && u.Id != id))
        return Results.Conflict(new MessageResponse($"Логин {request.Login} уже занят"));

    user.Login = request.Login!;
    user.PassHash = request.PassHash!.ToLowerInvariant();
    await db.SaveChangesAsync();
    return Results.Ok(new MessageResponse($"Пользователь {id} обновлён"));
});

app.MapDelete("/user/{id:int}", async (int id, UsersContext db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user == null)
        return Results.NotFound(new MessageResponse($"Пользователь {id} не найден"));

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok(new MessageResponse($"Пользователь {id} удалён"));
});

app.Run();
