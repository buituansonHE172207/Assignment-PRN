

using Assignment_PRN.Data;
using Assignment_PRN.Entities;

namespace Assignment_PRN.Repositories.Implementation;

public class RatingRepository : RepositoryBase<Rating, int>, IRatingRepository
{
    public RatingRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }
}