using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.Common.DTO;
using WSA.Microservice.Template.Application.Common.Exceptions;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Common.Wrappers;

namespace WSA.Microservice.Template.Application.Queries.Config
{
    public class GetConfig
    {
        public class Query : IRequest<Response<ConfigDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response<ConfigDto>>
        {
            private readonly IConfigRepository _configRepository;
            private readonly IMapper _mapper;

            public Handler(IConfigRepository configRepository, IMapper mapper)
            {
                _configRepository = configRepository;
                _mapper = mapper;
            }

            public async Task<Response<ConfigDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var config = await _configRepository.GetByIdAsync(request.Id);

                if (config == null)
                {
                    throw new ApiException($"Config not found.");
                }

                var configResponse = _mapper.Map<ConfigDto>(config);
                return new Response<ConfigDto>(configResponse);
            }
        }

    }
}
