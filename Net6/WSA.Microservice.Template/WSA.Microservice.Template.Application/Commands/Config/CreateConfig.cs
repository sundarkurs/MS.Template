using AutoMapper;
using FluentValidation;
using MediatR;
using WSA.Microservice.Template.Application.DTO;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Wrappers;

namespace WSA.Microservice.Template.Application.Commands.Config
{
    public class CreateConfig
    {
        public class Command : IRequest<Response<ConfigDto>>
        {
            public ConfigRequest Config { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response<ConfigDto>>
        {
            private readonly IConfigRepository _configRepository;
            private readonly IMapper _mapper;

            public Handler(IConfigRepository configRepository, IMapper mapper)
            {
                _configRepository = configRepository;
                _mapper = mapper;
            }

            public async Task<Response<ConfigDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var config = _mapper.Map<Domain.Entities.Config>(request.Config);

                var response = await _configRepository.AddAsync(config);

                var newAssetType = _mapper.Map<ConfigDto>(response);

                return new Response<ConfigDto>(newAssetType);
            }
        }

        public class Validation : AbstractValidator<Command>
        {
            private readonly IConfigRepository _configRepository;

            public Validation(IConfigRepository configRepository)
            {
                _configRepository = configRepository;

                RuleFor(p => p.Config.Name)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .MaximumLength(100).WithMessage("{PropertyName} must not exceed 50 characters.")
                    .MustAsync(IsUniqueCode).WithMessage("{PropertyName} already exists.");
            }

            private async Task<bool> IsUniqueCode(string name, CancellationToken cancellationToken)
            {
                var response = await _configRepository.IsNameUniqueAsync(name);
                return response;
            }
        }
    }
}
