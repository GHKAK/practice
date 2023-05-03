using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace WeatherForecast.Commands {
    public record PutItemCommand(long Id, TodoItemDTO TodoDTO) : IRequest<bool>;
}
