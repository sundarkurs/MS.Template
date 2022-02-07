using AutoMapper;
using Moq;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
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
        }

        [Fact]
        public async void DeleteTodo_ExistingTodo_DeletedWithSuccess()
        {

        }

        [Fact]
        public async void DeleteTodo_InvalidTodo_ReturnsError()
        {

        }
    }
}
