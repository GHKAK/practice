using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using WeatherForecast.Commands;
using WeatherForecast.Queries;

namespace WeatherForecast.Handlers {
    public class PostItemHandler : IRequestHandler<PostItemCommand,TodoItem> {
        private TodoContext _context;
        public PostItemHandler(TodoContext context) {
            _context = context;
        }

        public async Task<TodoItem> Handle(PostItemCommand request, CancellationToken cancellationToken) {
            var todoItem = new TodoItem {
                IsComplete = request.TodoDTO.IsComplete,
                Name = request.TodoDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
    }
}