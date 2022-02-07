using AutoMapper;
using Moq;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Application.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Application.Tests.Queries.Todo
{
    public class GetTodoTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _repository;
        private readonly Domain.Entities.Todo _entity;

        public GetTodoTest()
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
        public async void GetTodo_ValidTodoIdentifier_ReturnsDataWithSuccess()
        {
            // Arrange
            _repository.Setup(x => x.GetByIdAsync(_entity.Id)).ReturnsAsync(_entity);
            var query = TodoMock.MockGetTodoQuery(_entity.Id);
            var handler = new GetTodo.Handler(_repository.Object, _mapper);

            // Act
            var response = await handler.Handle(query, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.Title, TodoMock.TodoEntity.Title);
        }
    }
}
