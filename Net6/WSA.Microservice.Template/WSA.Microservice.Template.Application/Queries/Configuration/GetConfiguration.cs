using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.DTO;
using WSA.Microservice.Template.Application.Exceptions;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Wrappers;

namespace WSA.Microservice.Template.Application.Queries.Configuration
{
    public class GetConfiguration
    {
        public class Query : IRequest<Response<ConfigurationDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<ConfigurationDto>>
        {
            private readonly IConfigurationRepository _configurationRepository;
            private readonly IMapper _mapper;

            public Handler(IConfigurationRepository configurationRepository, IMapper mapper)
            {
                _configurationRepository = configurationRepository;
                _mapper = mapper;
            }

            public async Task<Response<ConfigurationDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var configuration = await _configurationRepository.GetByIdAsync(request.Id);

                if (configuration == null)
                {
                    throw new ApiException($"Configuration not found.");
                }

                var configurationResponse = _mapper.Map<ConfigurationDto>(configuration);
                return new Response<ConfigurationDto>(configurationResponse);
            }
        }

    }
}
