using MediatR;
using TodoApi.Models;

namespace WeatherForecast.Queries {
    public record GetItemsQuery : IRequest<IEnumerable<TodoItemDTO>>;
}
