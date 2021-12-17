using AutoMapper;
using Moq;
using System.Linq;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;
using WSA.Microservice.Template.Application.Queries.Todo;
using WSA.Microservice.Template.Integration.Tests.Mock;
using Xunit;

namespace WSA.Microservice.Template.Integration.Tests.Queries.Todo
{
    public class GetTodoTest
    {
        private static IMapper _mapper;

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
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        public async void GetAllTodo_ReturnsAllTodos(int todoExist, int todoSearch, bool succeeded)
        {
            // Arrange
            var todoRepo = new Mock<ITodoRepository>();

            todoRepo.Setup(x => x.GetByIdAsync(todoExist)).ReturnsAsync(TodoMock.Todos.Single(x => x.Id == todoExist));

            var query = new GetTodo.Query() { Id = todoSearch };

            var handler = new GetTodo.Handler(todoRepo.Object, _mapper);

            // Act
            try
            {
                var response = await handler.Handle(query, new System.Threading.CancellationToken());

                // Assert
                Assert.NotNull(response);
                Assert.True(response.Succeeded == succeeded);
            }
            catch (Application.Common.Exceptions.ApiException e)
            {
                Assert.Equal("Todo not found.", e.Message);
            }

        }
    }
}
