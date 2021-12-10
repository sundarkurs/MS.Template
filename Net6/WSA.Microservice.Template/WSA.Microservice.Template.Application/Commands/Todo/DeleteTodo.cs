using AutoMapper;
using FluentValidation;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo
{
    public class DeleteTodo
    {
        public class Command : IRequest<Response<bool>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<bool>>
        {
            private readonly ITodoRepository _todoRepository;

            public Handler(ITodoRepository todoRepository)
            {
                _todoRepository = todoRepository;
            }

            public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
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
}
