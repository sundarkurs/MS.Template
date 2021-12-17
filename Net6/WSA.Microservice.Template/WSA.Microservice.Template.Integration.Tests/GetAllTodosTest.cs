using AutoMapper;
using MediatR;
using Moq;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Queries.Todo;
using Xunit;

namespace WSA.Microservice.Template.Integration.Tests
{
    public class GetAllTodosTest
    {
        [Fact]
        public async void GetAllTodos_ReturnsAllTodos()
        {
            var mediator = new Mock<IMediator>();

            var todoRepo = new Mock<ITodoRepository>();

            var mapper = new Mock<IMapper>();

            var query = new GetAllTodos.Query();

            var handler = new GetAllTodos.Handler(todoRepo.Object, mapper.Object);

            var resp = await handler.Handle(query, new System.Threading.CancellationToken());

            Assert.NotNull(resp);

        }
    }
}