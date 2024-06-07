using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;

namespace EmployeeDirectory.BAL.Providers
{
    public class ManagerProvider(IManagerRepository ManagerRepository) : IManagerProvider
    {
        public static Dictionary<int, string> Managers = new();
        private readonly IManagerRepository _ManagerRepository = ManagerRepository;

        public async Task GetManagers()
        {
            List<Manager>managers = await _ManagerRepository.GetAll();
            foreach (Manager manager in managers)
            {
                Managers.Add(manager.Id, manager.Name);
            }
        }
    }
}
