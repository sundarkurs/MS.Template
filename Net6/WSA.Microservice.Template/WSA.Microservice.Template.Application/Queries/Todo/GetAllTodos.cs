using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Queries.Todo
{
    public class GetAllTodos
    {
        public class Query : IRequest<PagedResponse<IEnumerable<TodoDto>>> { }

        public class Handler : IRequestHandler<Query, PagedResponse<IEnumerable<TodoDto>>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public Handler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<IEnumerable<TodoDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todos = await _todoRepository.GetAllAsync();
                var todosResponse = _mapper.Map<IEnumerable<TodoDto>>(todos);
                return new PagedResponse<IEnumerable<TodoDto>>(todosResponse, 1, 10);

            }
        }
    }
}
