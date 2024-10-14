namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201
    }

    [HttpGet]
    public IActionResult Read()
    {
        // try
        // {
        //     throw new Exception("Neco se pokazilo");
        // }
        // catch(Exception ex)
        // {
        //     return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        // }

        try
        {
            return Ok(items);
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);

        if (item == null)
        {
            return NotFound();
        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.IsCompleted = request.IsCompleted;
        return Ok(item);
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        var item = items.FirstOrDefault(i => i.ToDoItemId == toDoItemId);

        if (item == null)
        {
            return NotFound();
        }

        items.Remove(item);
        return NoContent();
    }
}
