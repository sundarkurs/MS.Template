using AutoMapper;
using Moq;
using System.Linq;
using WSA.Microservice.Template.Application.Commands.Todo.Create;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Integration.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Integration.Tests.Commands.Todo
{
    public class CreateTodoTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _todoRepository;
        private readonly Domain.Entities.Todo _expectedTodo;
        private readonly CreateTodoCommand _createTodoCommand;

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

            _todoRepository = new Mock<ITodoRepository>();
            _expectedTodo = TodoMock.NewTodo;
            _createTodoCommand = TodoMock.CreateCommand(_expectedTodo.Title, _expectedTodo.Description);
        }

        [Theory]
        [InlineData("Meet up", true, true)]
        [InlineData("Test", false, false)]
        public async void CreateTodo_Validate_Title(string title, bool expected, bool actual)
        {
            _todoRepository.Setup(x => x.IsTitleUniqueAsync(title)).ReturnsAsync(expected);

            var validator = new CreateTodoCommandValidator(_todoRepository.Object);

            // Act
            var valid = await validator.ValidateAsync(_createTodoCommand);

            // Assert
            Assert.True(expected == actual);
        }

        [Fact]
        public async void CreateTodo_ValidInput_CreateSucceed()
        {
            // Arrange
            _todoRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>())).ReturnsAsync(_expectedTodo);

            var validator = new CreateTodoCommandValidator(_todoRepository.Object);

            var handler = new CreateTodoCommandHandler(_todoRepository.Object, _mapper);

            // Act
            var response = await handler.Handle(_createTodoCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(_expectedTodo.Title, response.Data.Title);

        }

        [Fact]
        public async void CreateTodo_AddFails_CreateFails()
        {
            // Arrange
            _todoRepository.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>())).ReturnsAsync((Domain.Entities.Todo)null);

            var validator = new CreateTodoCommandValidator(_todoRepository.Object);

            var handler = new CreateTodoCommandHandler(_todoRepository.Object, _mapper);

            // Act
            var response = await handler.Handle(_createTodoCommand, new System.Threading.CancellationToken());

            // Assert
            Assert.Null(response.Data);
        }
    }
}
