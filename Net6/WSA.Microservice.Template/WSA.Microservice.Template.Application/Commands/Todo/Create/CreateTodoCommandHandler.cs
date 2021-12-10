using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Create
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Response<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public CreateTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Response<TodoDto>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<Domain.Entities.Todo>(request.Todo);

            var response = await _todoRepository.AddAsync(todo);

            var todoResponse = _mapper.Map<TodoDto>(response);

            return new Response<TodoDto>(todoResponse);
        }
    }
}
