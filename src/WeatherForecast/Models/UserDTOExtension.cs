using TodoApi.Models;

namespace WeatherForecast.Models {
    public static class UserDTOExtension {
        public static UserDTO UserToDTO(this User user) {
            var todosDto = user.TodoItems.Select(u => u.ItemToDTO()).ToList();
            return new UserDTO {
                Id = user.Id,
                UserName = user.UserName,
                TodoItems = todosDto,
            };

        }
    }
}
