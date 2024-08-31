using Assignment_PRN.Data;
using Assignment_PRN.Entities;

namespace Assignment_PRN.Repositories.Implementation;

public class EpisodeRepository : RepositoryBase<Episode, int>, IEpisodeRepository
{
    public EpisodeRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }
}