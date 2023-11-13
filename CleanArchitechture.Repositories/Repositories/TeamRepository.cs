using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Interfaces.Repositories;

namespace CleanArchitechture.Repositories.Repositories;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(AppDbContext context) : base(context)
    {
    }
}