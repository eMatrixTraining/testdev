using TestDev.Data.Entities;

namespace TestDev.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<ApplicationUser> ApplicationUsers { get; }

    }
}
