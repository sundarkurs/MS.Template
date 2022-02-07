using AutoMapper;
using Moq;
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

            var model = TodoMock.TodoModel;
            var command = TodoMock.GetCreateTodoCommand(model);
            var handler = new CreateTodoCommandHandler(_repository.Object, _mapper);

            // Act
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(_entity.Title, response.Data.Title);
        }

        [Fact]
        public async void CreateTodo_DuplicateTodo_ReturnsError()
        {

        }

    }
}
