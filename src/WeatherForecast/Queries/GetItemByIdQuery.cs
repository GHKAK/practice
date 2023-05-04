using MediatR;
using TodoApi.Models;

namespace WeatherForecast.Queries {
    public record GetItemByIdQuery(long Id):IRequest<TodoItemDTO>;
}
