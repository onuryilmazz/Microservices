using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", EndpointOperations.GetAllTodos);

app.MapGet("/complete", EndpointOperations.GetCompleteTodos);
app.MapGet("/{id}", EndpointOperations.GetTodo);
app.MapPost("/", EndpointOperations.CreateTodo);
app.MapPut("/{id}", EndpointOperations.UpdateTodo);
app.MapDelete("/{id}", EndpointOperations.DeleteTodo);

app.Run();