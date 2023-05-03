using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using WeatherForecast.Models;
using WeatherForecast.Queries;

namespace WeatherForecast.Handlers {
    public class GetItemsHandler:IRequestHandler<GetItemsQuery,IEnumerable<TodoItemDTO>> {
        private TodoContext _context;
        public GetItemsHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItemDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken) {
            return await _context.TodoItems
                        .Select(x => x.ItemToDTO())
                        .ToListAsync();
        }
    }
}
