using efproyecto;
using efproyecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
// Generar base de datos en SQL Server
// El string connection está en appsettings.json y aquí solo se invoca. Esto sobre todo para evitar problemas de seguridad
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/dbconection", ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    // return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsSqlServer());
});

// Mapeo de una nueva ruta
// Obtener datos de la tabla tarea
app.MapGet("/api/todos", ([FromServices] TareasContext dbContext) =>
{
    // Con la linea de abajo traemos todas las tareas en formato JSON
    // return Results.Ok(dbContext.Tareas);

    // Pequeño filtro con .Where
    // Con la linea de abajo traemos todas las tareas con prioridad alta
    return Results.Ok(dbContext.Todos
                               .Include(t => t.Category)
                               .Where(t => t.TodoPriority == Priority.High));
});

// Hacer post de una nueva tarea
// Con [FromBody] Estamos diciendo que vamos a coger el cuerpo de un modelo, en este caso Tarea
app.MapPost("/api/todos", async ([FromServices] TareasContext dbContext, [FromBody] Todo todo) =>
{
    todo.CategoryId = Guid.NewGuid();
    todo.CreationDate = DateTime.Now;
    await dbContext.AddAsync(todo);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

// Hacer PUT de una nueva tarea
// Con [FromBody] Estamos diciendo que vamos a coger el cuerpo de un modelo, en este caso Tarea
// Con [FromRoute] especificamos que vamos a usar el Guid id para seleccionar la tarea a modificar
// En la ruta hay que especificar que tiene que usar el id de la tarea en cuestión
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Todo todo, [FromRoute] Guid id) =>
{
    var currentTodo = dbContext.Todos.Find(id);
    if(currentTodo != null)
    {
        currentTodo.CategoryId = todo.CategoryId;
        currentTodo.Title = todo.Title;
        currentTodo.TodoPriority = todo.TodoPriority;
        currentTodo.Description = todo.Description;
        currentTodo.Weight = todo.Weight;

        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

// Hacer DELETE de una nueva tarea
// Con [FromBody] Estamos diciendo que vamos a coger el cuerpo de un modelo, en este caso Tarea
// En este caso el body no lo necesitamos pero si el Guid id para saber qué tarea se va a eliminar
app.MapDelete("/api/todos/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    var currentTodo = dbContext.Todos.Find(id);
    if(currentTodo != null)
    {
        dbContext.Remove(currentTodo);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.Run();
