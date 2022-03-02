using AutoMapper;
using Moq;
using System;
using WSA.Microservice.Template.Application.Commands.Todo.Create;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Application.Tests.Commands.Todo
{
    public class CreateTodoTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _repository;
        private readonly Domain.Entities.Todo _entity;

        public CreateTodoTest()
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
        public async void CreateTodo_NewTodo_CreatedWithSuccess()
        {
            // Arrange
            _repository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>())).ReturnsAsync(_entity);
            _repository.Setup(x => x.IsTitleUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);

            var model = TodoMock.TodoModel;
            var command = TodoMock.GetCreateTodoCommand(model);
            var validator = new CreateTodoCommandValidator(_repository.Object);
            var handler = new CreateTodoCommandHandler(_repository.Object, _mapper);

            // Act

            var validationResult = await validator.ValidateAsync(command);
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(_entity.Title, response.Data.Title);
        }

        [Theory]
        [InlineData("Meet up", true, true)]
        [InlineData("Test", false, false)]
        public async void CreateTodo_Validate_Title(string title, bool isValidExpected, bool isValidActual)
        {
            // Arrange
            _repository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>())).ReturnsAsync(_entity);
            _repository.Setup(x => x.IsTitleUniqueAsync(title)).ReturnsAsync(isValidExpected);

            var model = TodoMock.TodoModel;
            var command = TodoMock.GetCreateTodoCommand(model);

            var validator = new CreateTodoCommandValidator(_repository.Object);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            Assert.NotNull(validationResult);
            Assert.True(isValidActual == validationResult.IsValid);
            if (!validationResult.IsValid)
            {
                Assert.Equal("Todo Title already exists.", validationResult.Errors[0].ErrorMessage);
            }

        }
    }
}
