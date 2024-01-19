using Microsoft.EntityFrameworkCore;
using TodoMinimal.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoItems"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/todo", async (TodoDb db) => 
    await db.Todos.ToListAsync());

app.MapGet("/todo/completed", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsCompleted).ToListAsync());


app.MapGet("/todo/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/todo", async (TodoBody todoBody, TodoDb db) =>
{
    var todo = new Todo
    {
        TodoTask = todoBody.TodoTask,
        IsCompleted = false
    };
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todo/{todo.Id}", todo);
});

app.MapPut("/todo/{id}", async (int id, TodoBody todoBody, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.TodoTask = todoBody.TodoTask;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/todo/complete/{id}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.IsCompleted = true;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todo", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
