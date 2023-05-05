using TodoApi.Models;

namespace TodoApi.Repository;

public interface IUsersRepository {
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<UserDTO> GetUserById(long userId);
    Task<IEnumerable<TodoItemDTO>> GetTodoItemsForUser(long userId);
    Task<UserDTO> AddUser(UserDTO userDTO);
    Task<TodoItem> AddTodoForUser(TodoItemDTO todoItemDTO,long userId);
    Task<bool> UpdateUser(UserDTO userDTO);
    Task<bool> DeleteUser(long userId);
}