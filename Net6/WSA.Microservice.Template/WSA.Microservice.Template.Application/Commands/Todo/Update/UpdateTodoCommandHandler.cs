using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Update
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Response<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public UpdateTodoCommandHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Response<TodoDto>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);

            if (todo == null)
            {
                throw new ApiException($"Todo not found.");
            }

            todo.Title = request.Todo.Title;
            todo.Description = request.Todo.Description;

            await _todoRepository.UpdateAsync(todo);

            var todoResponse = _mapper.Map<TodoDto>(todo);

            return new Response<TodoDto>(todoResponse);
        }
    }
}
