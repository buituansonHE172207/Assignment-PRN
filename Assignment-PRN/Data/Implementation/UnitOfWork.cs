using Assignment_PRN.Repositories;

namespace Assignment_PRN.Data.Implementation;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly AssignmentPRNContext _dbContext;

    public UnitOfWork(AssignmentPRNContext appDbContext, ICountryRepository countryRepository,
        ICommentRepository commentRepository, IEpisodeRepository episodeRepository, IGenreRepository genreRepository,
        IFilmRepository filmRepository, IRatingRepository ratingRepository, IUserRepository userRepository,
        ITransactionRepository transactionRepository)
    {
        _dbContext = appDbContext;
        Countries = countryRepository;
        Comments = commentRepository;
        Episodes = episodeRepository;
        Genres = genreRepository;
        Films = filmRepository;
        Ratings = ratingRepository;
        Transactions = transactionRepository;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public ICommentRepository Comments { get; }
    public ICountryRepository Countries { get; }
    public IEpisodeRepository Episodes { get; }
    public IGenreRepository Genres { get; }
    public IFilmRepository Films { get; }
    public IRatingRepository Ratings { get; }
    public ITransactionRepository Transactions { get; }

    public bool Commit()
    {
        using var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            var result = _dbContext.SaveChanges();
            transaction.Commit();
            return result > 0;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result > 0;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}