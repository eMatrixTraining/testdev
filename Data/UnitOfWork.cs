using TestDev.Data.Entities;
using TestDev.Data.Interfaces;
using TestDev.Data.Repositories;

namespace TestDev.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Repository<ApplicationUser> _applicationUser;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<ApplicationUser> ApplicationUsers => _applicationUser ?? new Repository<ApplicationUser>(_dbContext);
    }
}