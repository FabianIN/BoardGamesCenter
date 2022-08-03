using BoardGamesCenter.Services.Repositories;

namespace BoardGamesCenter.Services.UnitsOfWork
{
    public interface IUserUnitOfWork :  IDisposable
    {
        IUserRepository Users { get; }

        int Complete();

    }
}
