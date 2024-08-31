using Assignment_PRN.Repositories;

namespace Assignment_PRN.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        ICountryRepository Countries { get; }
        IEpisodeRepository Episodes { get; }
        IGenreRepository Genres { get; }
        IFilmRepository Films { get; }
        IRatingRepository Ratings { get; }
        ITransactionRepository Transactions { get; }
        bool Commit();
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
