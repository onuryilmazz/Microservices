using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi;

internal static class EndpointOperations
{
    internal static async Task<IResult> GetAllTodos(TodoDbContext db)
    {
        return TypedResults.Ok(await db.Todos.ToArrayAsync());
    }

    internal static async Task<IResult> GetCompleteTodos(TodoDbContext db)
    {
        return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).ToListAsync());
    }

    internal static async Task<IResult> GetTodo(int id, TodoDbContext db)
    {
        return await db.Todos.FindAsync(id)
            is Todo todo
                ? TypedResults.Ok(todo)
                : TypedResults.NotFound();
    }

    internal static async Task<IResult> CreateTodo(Todo todo, TodoDbContext db)
    {
        db.Todos.Add(todo);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/todoitems/{todo.Id}", todo);
    }

    internal static async Task<IResult> UpdateTodo(int id, Todo inputTodo, TodoDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);

        if (todo is null) return TypedResults.NotFound();

        todo.Name = inputTodo.Name;
        todo.IsComplete = inputTodo.IsComplete;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    internal static async Task<IResult> DeleteTodo(int id, TodoDbContext db)
    {
        if (await db.Todos.FindAsync(id) is Todo todo)
        {
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return TypedResults.Ok(todo);
        }

        return TypedResults.NotFound();
    }
}
