using MediatR;
using TodoApi.Models;
using WeatherForecast.Commands;

namespace WeatherForecast.Handlers {
    public class DeleteItemHandler:IRequestHandler<DeleteItemCommand,bool> {
        private TodoContext _context;
        public DeleteItemHandler(IMediator mediator, TodoContext todoContext) {
            _context = todoContext;
        }

        public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken) {
            var todoItem = await _context.TodoItems.FindAsync(request.Id);
            if (todoItem == null) {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}