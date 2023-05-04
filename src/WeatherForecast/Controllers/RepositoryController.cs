using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repository;
using WeatherForecast.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepositoryController : ControllerBase {
    private IUsersRepository _repository;
    public RepositoryController(IUsersRepository repository) {
        _repository = repository;
    }

    // GET: api/TodoItems
    [HttpGet("GetUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
        var users = await _repository.GetUsers();
        return Ok(users);
    }

    //// POST: api/TodoItems
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //// <snippet_Create>
    [HttpPost("PostUser")]
    public async Task<ActionResult<User>> PostUser(UserDTO user) {
        var userAdded = await _repository.AddUser(user);
        return Ok(userAdded);
    }
    //</snippet_Create>

    //GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("GetUserById{userId}")]
    public async Task<ActionResult<User>> GetUser(long userId) {
        var userFounded = await _repository.GetUserById(userId);
        return Ok(userFounded);
    }
    //</snippet_GetByID>

    //GET: api/TodoItems/5
    // <snippet_GetTodos>
    [HttpGet("GetTodosForUser{userId}")]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItemsForUser(long userId) {
        var todos = await _repository.GetTodoItemsForUser(userId);
        return Ok(todos);
    }
    //</snippet_GetTodos>
    // DELETE: api/TodoItems/5
    [HttpDelete("DeleteUser{userId}")]
    public async Task<IActionResult> DeleteUser(long userId) {

        var isDeleted = await _repository.DeleteUser(userId);
        if (!isDeleted) {
            return NotFound();
        }
        return NoContent();
    }
    //</snippet_DeleteUser>

    // DELETE: api/TodoItems/5
    [HttpPost("PostTodoForUser{userId}")]
    public async Task<ActionResult<TodoItemDTO>> PostTodo(TodoItemDTO todo, long userId) {
        try { 
        var item = await _repository.AddTodoForUser(todo,userId);
        return Ok(item.ItemToDTO());
        }
        catch (ArgumentException ex) {
            return NotFound(ex.Message);
        }
    }
    //</snippet_Create>
}

