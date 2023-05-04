using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using WeatherForecast.Models;

namespace TodoApi.Repository;

public class UsersRepository : IUsersRepository {
    private TodoContext _context;
    public UsersRepository(TodoContext context) {
        _context = context;
    }
    public async Task<TodoItem> AddTodoForUser(TodoItemDTO todoItemDTO, long userId) {
        var user = await GetUserById(userId);
        if (user == null) {
            throw new ArgumentException("There is not such user");
        }
        var todoItem = new TodoItem {
            IsComplete = todoItemDTO.IsComplete,
            Name = todoItemDTO.Name,
            User = user,
        };
        user.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<User> AddUser(UserDTO userDTO) {
        var newUser= new User() { Id = userDTO.Id, UserName = userDTO.UserName };
        _context.Users.Add(newUser) ;
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task<bool> DeleteUser(long userId) {
        var user = await GetUserById(userId);
        if (user == null) {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsForUser(long userId) {
        var todos = await _context.TodoItems.Where(ti => ti.UserId == userId).ToListAsync();
        return from i in todos
               select i.ItemToDTO(); ;
    }

    public async Task<User> GetUserById(long userId) {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<User>> GetUsers() {
        return await _context.Users.Select(x=>x).ToListAsync();
    }

    public void UpdateUser() {
        throw new NotImplementedException();
    }
}