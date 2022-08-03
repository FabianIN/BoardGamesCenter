using BoardGamesCenter.Services.Repositories;

namespace BoardGamesCenter.Services.UnitsOfWork
{
    public interface IGameUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }

        IAuthorRepository Authors { get; }

        IPublisherRepository Publishers { get; }

        int Complete();
    }
}
