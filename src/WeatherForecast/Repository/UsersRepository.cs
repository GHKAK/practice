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
        var user = await GetUser(userId);
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

    public async Task<UserDTO> AddUser(UserDTO userDTO) {
        var newUser = new User() { Id = userDTO.Id, UserName = userDTO.UserName };
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return newUser.UserToDTO();
    }

    public async Task<bool> DeleteUser(long userId) {
        var user = await GetUser(userId);
        if (user == null) {
            return false;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsForUser(long userId) {
        var todos = await _context.TodoItems.Where(ti => ti.UserId == userId).Include(x => x.User).ToListAsync();
        return from i in todos
               select i.ItemToDTO(); 
    }

    public async Task<UserDTO> GetUserById(long userId) {
        var x = await GetUser(userId);
        return x.UserToDTO();
    }

    public async Task<IEnumerable<UserDTO>> GetUsers() {
        var x = await _context.Users.Select(x => x).Include(x => x.TodoItems).ToListAsync();
        return x.Select(u=>u.UserToDTO());
    }

    public async Task<bool> UpdateUser(UserDTO userDTO) {
        var user = await GetUser(userDTO.Id);
        if (user == null) {
            return false;
        }
        user.Id = userDTO.Id;
        user.UserName = userDTO.UserName;
        await _context.SaveChangesAsync();
        return true;
    }
    private async Task<User> GetUser(long id) {
        return await _context.Users.FindAsync(id);
    }
}