using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace WeatherForecast.Commands {
    public record DeleteItemCommand(long Id) : IRequest<bool>;
}
