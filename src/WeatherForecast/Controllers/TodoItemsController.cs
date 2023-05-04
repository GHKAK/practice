using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using WeatherForecast.Commands;
using WeatherForecast.Queries;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase {
    private readonly IMediator _mediator;
    public TodoItemsController(IMediator mediator) {
        _mediator = mediator;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<IEnumerable<TodoItemDTO>> GetTodoItems() {
        return await _mediator.Send(new GetItemsQuery());
    }

    //GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<TodoItemDTO> GetTodoItemByID(long id) { 
        return await _mediator.Send(new GetItemByIdQuery(id));
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoDTO) {
        if(id != todoDTO.Id) {
            return BadRequest();
        }
        var isPuted = await _mediator.Send(new PutItemCommand(id, todoDTO));
        if(!isPuted) {
            return NotFound();
        }
        return NoContent();
    }
    // </snippet_Update>

    //// POST: api/TodoItems
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //// <snippet_Create>
    //[HttpPost]
    //public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO) {
    //    var todoItem = new TodoItem {
    //        IsComplete = todoDTO.IsComplete,
    //        Name = todoDTO.Name
    //    };

    //    _context.TodoItems.Add(todoItem);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(
    //        nameof(GetTodoItem),
    //        new { id = todoItem.Id },
    //        ItemToDTO(todoItem));
    //}
    //// </snippet_Create>

    //// DELETE: api/TodoItems/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteTodoItem(long id) {
    //    var todoItem = await _context.TodoItems.FindAsync(id);
    //    if (todoItem == null) {
    //        return NotFound();
    //    }

    //    _context.TodoItems.Remove(todoItem);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool TodoItemExists(long id) {
    //    return _context.TodoItems.Any(e => e.Id == id);
    //}

}