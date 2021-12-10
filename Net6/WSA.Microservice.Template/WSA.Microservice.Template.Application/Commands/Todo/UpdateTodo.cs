using AutoMapper;
using FluentValidation;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Todo
{
    public class UpdateTodo
    {
        public class Command : IRequest<Response<TodoDto>>
        {
            public int Id { get; set; }
            public TodoRequest Todo { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<TodoDto>>
        {
            private readonly ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public Handler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository;
                _mapper = mapper;
            }

            public async Task<Response<TodoDto>> Handle(Command request, CancellationToken cancellationToken)
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

        public class Validation : AbstractValidator<Command>
        {
            private readonly ITodoRepository _todoRepository;

            public Validation(ITodoRepository todoRepository)
            {
                _todoRepository = todoRepository;

                RuleFor(p => p.Todo.Title)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .MaximumLength(100).WithMessage("{PropertyName} must not exceed 50 characters.")
                    .MustAsync(IsUniqueTitle).WithMessage("{PropertyName} already exists.");
            }

            private async Task<bool> IsUniqueTitle(string name, CancellationToken cancellationToken)
            {
                var response = await _todoRepository.IsTitleUniqueAsync(name);
                return response;
            }
        }
    }
}
