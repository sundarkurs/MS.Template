using AutoMapper;
using Moq;
using System.Linq;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Integration.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Integration.Tests.Queries.Todo
{
    public class GetAllTodosTest
    {
        private static IMapper _mapper;

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
        }

        [Fact]
        public async void GetAllTodos_ReturnsAllTodos()
        {
            // Arrange

            var todoRepo = new Mock<ITodoRepository>();

            todoRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(TodoMock.Todos);

            var query = new GetAllTodos.Query();

            var handler = new GetAllTodos.Handler(todoRepo.Object, _mapper);

            // Act
            var response = await handler.Handle(query, new System.Threading.CancellationToken());

            // Assert

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.True(response.Data.ToList().Count > 0);

        }
    }
}