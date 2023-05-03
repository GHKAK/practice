using TodoApi.Models;

namespace WeatherForecast.Models {
    public static class ItemToDTOExtension {
        public static TodoItemDTO ItemToDTO(this TodoItem todoItem) =>
       new TodoItemDTO {
           Id = todoItem.Id,
           Name = todoItem.Name,
           IsComplete = todoItem.IsComplete
       };
    }
}
