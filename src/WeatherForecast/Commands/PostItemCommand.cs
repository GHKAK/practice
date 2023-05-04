using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace WeatherForecast.Commands {
    public record PostItemCommand(TodoItemDTO TodoDTO) : IRequest<TodoItem>;
}
