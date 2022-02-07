using FluentValidation;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;

namespace WSA.Microservice.Template.Application.Commands.Todo.Update
{
    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoCommandValidator(ITodoRepository todoRepository)
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
