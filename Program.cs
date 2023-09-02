using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;
using projectef.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("chainSQL"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconecction", async ([FromServices] TasksContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Database in memory: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TasksContext dbContext) =>{

    return Results.Ok(dbContext.Tareas.Include(p => p.Category).Where(p=> p.JobPriority == projectef.Models.Priority.Low));
});

app.MapPost("/api/tareas", async ([FromServices] TasksContext dbContext, [FromBody] Job job) => {

    job.JobId = Guid.NewGuid();
    job.CreationDate = DateTime.Now;
    await dbContext.AddAsync(job);
    // await dbContext.Tareas.AddAsync(job);
    await dbContext.SaveChangesAsync();

    return Results.Ok(dbContext.Tareas.Include(p => p.Category).Where(p => p.JobPriority == projectef.Models.Priority.Low));
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TasksContext dbContext, [FromBody] Job job, [FromRoute] Guid id) =>
{

    var actualJob = dbContext.Tareas.Find(id);

    if (actualJob != null)
    {
        actualJob.CategoryId = job.CategoryId;
        actualJob.Title = job.Title;
        actualJob.JobPriority = job.JobPriority;
        actualJob.Description = job.Description;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
    var actualJob = dbContext.Tareas.Find(id);

    if(actualJob != null)
    {
        dbContext.Remove(actualJob);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});


app.Run();
