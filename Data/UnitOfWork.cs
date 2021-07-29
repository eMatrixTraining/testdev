using TestDev.Data.Entities;
using TestDev.Data.Interfaces;
using TestDev.Data.Repositories;

namespace TestDev.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Repository<ApplicationUser> _applicationUser;
        private Repository<Series> _series;
        private Repository<Module> _modules;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<ApplicationUser> ApplicationUsers => _applicationUser ?? new Repository<ApplicationUser>(_dbContext);
        public IRepository<Series> Series => _series ?? new Repository<Series>(_dbContext);
        public IRepository<Module> Modules => _modules ?? new Repository<Module>(_dbContext);
    }
}