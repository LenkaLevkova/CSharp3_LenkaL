using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
{
    //WebApi services
    builder.Services.AddControllers(); // pridalo ToDoItemsController
    builder.Services.AddSwaggerGen();

    //Persistence services
    builder.Services.AddDbContext<ToDoItemsContext>(); // pridalo ToDoItemsContext
    builder.Services.AddScoped<IRepositoryAsync<ToDoItem>, ToDoItemsRepository>(); // pridalo ToDoItemsRepository
}

var app = builder.Build();
{
    //Configure Middleware (HTTP request pipeline)
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1"));
}

// app.MapGet("/", () => "Ahoj!");
// app.MapGet("/nazdarSvete", () => "Nazdar svete lk!");

app.Run();
