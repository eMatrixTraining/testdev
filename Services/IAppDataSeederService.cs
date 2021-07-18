using System.Threading.Tasks;

namespace TestDev.Services
{
    public interface IAppDataSeederService
    {
        Task<bool> CreateDefaultUsers();
    }
}