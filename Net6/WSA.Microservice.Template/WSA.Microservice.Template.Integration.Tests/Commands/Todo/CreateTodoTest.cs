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
        }

        [Fact]
        public async void CreateTodo_Success()
        {
            // Arrange

            var todoRepo = new Mock<ITodoRepository>();

            var expected = new Domain.Entities.Todo()
            {
                Id = 1,
                Title = "Test",
                Description = "Test"
            };

            todoRepo.Setup(x => x.AddAsync(It.IsAny<Domain.Entities.Todo>())).ReturnsAsync(expected);

            todoRepo.Setup(x => x.IsTitleUniqueAsync(expected.Title)).ReturnsAsync(false);

            var command = new CreateTodoCommand
            {
                Todo = new Application.Common.DTO.TodoRequest { Title = expected.Title, Description = expected.Description }
            };

            var validator = new CreateTodoCommandValidator(todoRepo.Object);

            var handler = new CreateTodoCommandHandler(todoRepo.Object, _mapper);

            // Act
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert

            Assert.NotNull(response);
            Assert.True(response.Succeeded);
            Assert.NotNull(response.Data);
            Assert.Equal(expected.Title, response.Data.Title);

        }
    }
}
