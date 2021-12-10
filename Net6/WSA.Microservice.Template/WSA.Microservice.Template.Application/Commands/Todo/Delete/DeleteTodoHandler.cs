using MediatR;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo.Delete
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Response<bool>>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);

            if (todo == null)
            {
                throw new ApiException($"Todo not found.");
            }

            await _todoRepository.DeleteAsync(todo);

            return new Response<bool>(true);
        }
    }
}
