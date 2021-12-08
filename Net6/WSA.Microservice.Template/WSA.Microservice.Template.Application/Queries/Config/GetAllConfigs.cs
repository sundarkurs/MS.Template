using AutoMapper;
using MediatR;
using WSA.Microservice.Template.Application.DTO;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Application.Wrappers;

namespace WSA.Microservice.Template.Application.Queries.Config
{
    public class GetAllConfigs
    {
        public class Query : IRequest<PagedResponse<IEnumerable<ConfigDto>>> { }

        public class Handler : IRequestHandler<Query, PagedResponse<IEnumerable<ConfigDto>>>
        {
            private readonly IConfigRepository _configRepository;
            private readonly IMapper _mapper;

            public Handler(IConfigRepository configRepository, IMapper mapper)
            {
                _configRepository = configRepository;
                _mapper = mapper;
            }

            public async Task<PagedResponse<IEnumerable<ConfigDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var configs = await _configRepository.GetAllAsync();
                var configsResponse = _mapper.Map<IEnumerable<ConfigDto>>(configs);
                return new PagedResponse<IEnumerable<ConfigDto>>(configsResponse, 1, 10);

            }
        }
    }
}
