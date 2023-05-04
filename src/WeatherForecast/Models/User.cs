namespace TodoApi.Models;

public class User {
    public User() {
        TodoItems = new List<TodoItem>();
    }
    public long Id { get; set; }
    public string UserName { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; }
}