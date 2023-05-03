using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using WeatherForecast.Commands;
using WeatherForecast.Queries;

namespace WeatherForecast.Handlers {
    public class PutItemHandler:IRequestHandler<PutItemCommand,bool> {
        private IMediator _mediator;
        private TodoContext _context;
        public PutItemHandler(IMediator mediator, TodoContext todoContext) {
            _mediator = mediator;
            _context = todoContext;
        }

        public async Task<bool> Handle(PutItemCommand request, CancellationToken cancellationToken) {
            var item = await _mediator.Send(new GetItemByIdQuery(request.Id));
            if(item == null) {
                return false;
            }
            item.Name = request.TodoDTO.Name;
            item.IsComplete = request.TodoDTO.IsComplete;
            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) when(!_context.TodoItems.Any(e => e.Id == request.Id)) {
                return false;
            }
            return true;
        }
    }
}