using MediatR;
using TodoApi.Models;
using WeatherForecast.Queries;
using WeatherForecast.Models;

namespace WeatherForecast.Handlers {
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, TodoItemDTO> {
        private IMediator _mediator;
        public GetItemByIdHandler(IMediator mediator) {
            _mediator = mediator;
        }
        public async Task<TodoItemDTO> Handle(GetItemByIdQuery request, CancellationToken cancellationToken) {
            var items = await _mediator.Send(new GetItemsQuery());
            return items.FirstOrDefault(item => item.Id == request.Id);
        }
    }
}
