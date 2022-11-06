using IaaS2;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapGet("api/todos", (IToDoRepository repo) => {
    var toDos = repo.GetAllToDos();
    return Results.Ok(toDos);
});

app.MapGet("api/todos/{id}", (IToDoRepository repo, int id) => {
    var toDo = repo.GetToDoById(id);
    if (toDo != null)
    {
        return Results.Ok(toDo);
    }
    return Results.NotFound();
});

app.MapPost("api/todos", (IToDoRepository repo, ToDoCreateModel createToDo) => {

    var toDo = new ToDo();
    toDo.TaskDescription = createToDo.TaskDescription;

    repo.CreateToDo(toDo);

    return Results.Created($"api/todos/{toDo.Id}", toDo);
});

app.MapPut("api/todos/{id}", (IToDoRepository repo, int id, ToDoUpdateModel updateToDo) => {
    var toDo = repo.GetToDoById(id);
    if (toDo == null)
    {
        return Results.NotFound();
    }

    if (!string.IsNullOrWhiteSpace(updateToDo.TaskDescription))
    {
        toDo.TaskDescription = updateToDo.TaskDescription;
    }
    
    toDo.IsCompleted = updateToDo.IsCompleted;

    return Results.NoContent();
});

app.MapDelete("api/todos/{id}", (IToDoRepository repo, int id) => {
    var toDo = repo.GetToDoById(id);
    if (toDo == null)
    {
        return Results.NotFound();
    }

    repo.DeleteToDo(toDo);

    return Results.NoContent();
});

app.Run();
