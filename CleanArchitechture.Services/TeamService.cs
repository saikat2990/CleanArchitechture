using AutoMapper;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Interfaces.Repositories;
using CleanArchitechture.Core.Interfaces.Services;

namespace CleanArchitechture.Services
{
    public class TeamService : BaseService<Team, TeamDto>, ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public TeamService(ITeamRepository teamRepository, IMapper mapper) : base(teamRepository, mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        
    }
}
