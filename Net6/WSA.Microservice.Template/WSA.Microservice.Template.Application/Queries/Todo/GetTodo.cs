using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Queries.Todo
{
    public class GetTodo
    {
        public class Query : IRequest<Response<TodoDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<TodoDto>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public Handler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<Response<TodoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = await _todoRepository.GetByIdAsync(request.Id);

                if (todo == null)
                {
                    throw new ApiException($"Todo not found.");
                }

                var todoResponse = _mapper.Map<TodoDto>(todo);
                return new Response<TodoDto>(todoResponse);
            }
        }

    }
}
