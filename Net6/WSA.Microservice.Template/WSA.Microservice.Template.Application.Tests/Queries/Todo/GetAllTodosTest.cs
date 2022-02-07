using AutoMapper;
using Moq;
using System.Linq;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Application.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Application.Tests.Queries.Todo
{
    public class GetAllTodosTest
    {
        private static IMapper _mapper;
        private readonly Mock<ITodoRepository> _repository;

        public GetAllTodosTest()
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
        }

        [Fact]
        public async void GetAllTodos_ReturnsData()
        {
            // Arrange
            _repository.Setup(x => x.GetAllAsync()).ReturnsAsync(TodoMock.ListOfTodos);
            var query = TodoMock.MockGetAllTodosQuery();
            var handler = new GetAllTodos.Handler(_repository.Object, _mapper);

            // Act
            var response = await handler.Handle(query, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(response.Data.ToList().Count, TodoMock.ListOfTodos().Count);
        }
    }
}
