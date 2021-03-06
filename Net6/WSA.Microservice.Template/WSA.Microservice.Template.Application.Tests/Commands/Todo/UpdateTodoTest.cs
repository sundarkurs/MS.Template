using AutoMapper;
using Moq;
using WSA.Microservice.Template.Application.Commands.Todo.Create;
using WSA.Microservice.Template.Application.Commands.Todo.Update;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Application.Tests.Commands.Todo
{
    public class UpdateTodoTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _repository;
        private readonly Domain.Entities.Todo _entity;

        public UpdateTodoTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new Application.Common.Mappings.ApplicationProfile());
                });
                _mapper = mappingConfig.CreateMapper();
            }

            _repository = new Mock<ITodoRepository>();
            _entity = TodoMock.TodoEntity;
        }

        [Fact]
        public async void UpdateTodo_ValidTodo_UpdatedWithSuccess()
        {
            // Arrange
            _repository.Setup(x => x.GetByIdAsync(_entity.Id)).ReturnsAsync(_entity);
            _repository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Todo>()));
            _repository.Setup(x => x.IsTitleUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);

            var model = TodoMock.UpdateTodoModel;
            var command = TodoMock.GetUpdateTodoCommand(_entity.Id, model);
            var validator = new UpdateTodoCommandValidator(_repository.Object);
            var handler = new UpdateTodoCommandHandler(_repository.Object, _mapper);

            // Act
            var validationResult = await validator.ValidateAsync(command);
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async void UpdateTodo_DuplicateTitle_ThrowsValidationError()
        {
            // Arrange
            _repository.Setup(x => x.IsTitleUniqueAsync(_entity.Title)).ReturnsAsync(false);

            var model = TodoMock.UpdateTodoModel;
            var command = TodoMock.GetUpdateTodoCommand(_entity.Id, model);

            var validator = new UpdateTodoCommandValidator(_repository.Object);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Equal("Todo Title already exists.", validationResult.Errors[0].ErrorMessage);
        }
    }
}
