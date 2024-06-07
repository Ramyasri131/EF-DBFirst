using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Services;
using EmployeeDirectory.Manager;
using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.BAL.Providers;
using EmployeeDirectory.DAL.Data;
using EmployeeDirectory.DAL.Repository;
using Microsoft.Extensions.Configuration;
using EmployeeDirectory.DAL.Models;
using EmployeeDirectory.DAL.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeDirectory
{
    public class TestProgram
    {
        private static IDepartmentProvider _DepartmentProvider;
        private static IManagerProvider _ManagerProvider;
        private static IProjectProvider _ProjectProvider;
        private static ILocationProvider _LocationProvider;

        public TestProgram(IDepartmentProvider DepartmentProvider,ILocationProvider locationProvider,IManagerProvider managerProvider,IProjectProvider projectProvider)
        {
            _DepartmentProvider = DepartmentProvider;
            _ManagerProvider = managerProvider;
            _ProjectProvider = projectProvider;
            _LocationProvider  = locationProvider;
        }

        public static async Task Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IMenuManager, Menu>();
            services.AddScoped<IEmployeeProvider, EmployeeProvider>();
            services.AddScoped<IRoleProvider, RoleProvider>();
            services.AddScoped<IDepartmentProvider, DepartmentsProvider>();
            services.AddScoped<ILocationProvider, LocationProvider>();
            services.AddScoped<IManagerProvider, ManagerProvider>();
            services.AddScoped<IProjectProvider, ProjectsProvider>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            services.AddDbContext<RamyaEmployeeDirectoryDbContext>(options => configuration.GetConnectionString("EmployeeDirectoryDB"));

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IMenuManager menuManager = serviceProvider.GetRequiredService<IMenuManager>();
            await menuManager.DisplayMainMenu();
            await _DepartmentProvider.GetDepartments();
            await _LocationProvider.GetLocations();
            await _ManagerProvider.GetManagers();
            await _ProjectProvider.GetProjects();
        }
    }
}
