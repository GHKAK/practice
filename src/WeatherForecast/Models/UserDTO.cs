using TodoApi.Models;

public class UserDTO {
    public UserDTO() {

    }
    public long Id { get; set; }
    public string UserName { get; set; }
    public IEnumerable<TodoItemDTO> TodoItems { get; set; }
}