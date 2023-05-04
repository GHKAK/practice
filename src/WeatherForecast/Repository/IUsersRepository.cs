using TodoApi.Models;

namespace TodoApi.Repository;

public interface IUsersRepository {
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUserById(long userId);
    Task<IEnumerable<TodoItemDTO>> GetTodoItemsForUser(long userId);
    Task<User> AddUser(UserDTO userDTO);
    Task<TodoItem> AddTodoForUser(TodoItemDTO todoItemDTO,long userId);
    Task<bool> UpdateUser(UserDTO userDTO);
    Task<bool> DeleteUser(long userId);
}