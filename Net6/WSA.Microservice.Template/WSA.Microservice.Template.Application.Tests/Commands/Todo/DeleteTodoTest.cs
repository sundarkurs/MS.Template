using AutoMapper;
using Moq;
using WSA.Microservice.Template.Application.Commands.Todo.Delete;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Application.Tests.Commands.Todo
{
    public class DeleteTodoTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _repository;
        private readonly Domain.Entities.Todo _entity;

        public DeleteTodoTest()
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
        public async void DeleteTodo_ExistingTodo_DeletedWithSuccess()
        {
            // Arrange
            _repository.Setup(x => x.GetByIdAsync(_entity.Id)).ReturnsAsync(_entity);
            _repository.Setup(x => x.DeleteAsync(It.IsAny<Domain.Entities.Todo>()));

            var command = TodoMock.GetDeleteTodoCommand(_entity.Id);
            var handler = new DeleteTodoCommandHandler(_repository.Object);

            // Act
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
        }

        [Fact]
        public async void DeleteTodo_InvalidTodo_ReturnsError()
        {

        }
    }
}
