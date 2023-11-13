using AutoMapper;
using BookKeeping.Services;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Interfaces.Repositories;
using CleanArchitechture.Core.Interfaces.Services;

namespace CleanArchitechture.Services
{
    public class TeamService : BaseService<Team, TeamDto>, ITeamService
    {
        public readonly ITeamRepository _repo;
        public TeamService(ITeamRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
        }

    }
}
