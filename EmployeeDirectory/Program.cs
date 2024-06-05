using EmployeeDirectory.Manager;
using EmployeeDirectory.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EmployeeDirectory.BAL.Providers;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Data;
using EmployeeDirectory.DAL.Repository;
using Microsoft.Extensions.Configuration;
using EmployeeDirectory.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IEmployeeService, Services.EmployeeService>();
            serviceCollection.AddSingleton<IRoleService, Services.RoleService>();
            serviceCollection.AddSingleton<IMenuManager, Menu>();
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
            serviceCollection.AddScoped<IDepartmentRepository, DepartmentRepository>();
            serviceCollection.AddScoped<ILocationRepository, LocationRepository>();
            serviceCollection.AddScoped<IManagerRepository, ManagerRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            serviceCollection.AddSingleton<IEmployeeProvider, EmployeeProvider>();
            serviceCollection.AddSingleton<IRoleProvider, RoleProvider>();
            serviceCollection.AddSingleton<IDepartmentProvider, DepartmentsProvider>();
            serviceCollection.AddSingleton<ILocationProvider, LocationProvider>();
            serviceCollection.AddSingleton<IManagerProvider, ManagerProvider>();
            serviceCollection.AddSingleton<IProjectProvider, ProjectsProvider>();
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            serviceCollection.AddDbContext<RamyaEmployeeDirectoryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("EmployeeDirectoryDB")));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IMenuManager displayOptions = serviceProvider.GetService<IMenuManager>()!;
            IDepartmentProvider departmentProvider = serviceProvider.GetService<IDepartmentProvider>()!;
            await departmentProvider.GetDepartments();
            ILocationProvider locationProvider= serviceProvider.GetService<ILocationProvider>()!;
            await locationProvider.GetLocations();
            IManagerProvider managerProvider = serviceProvider.GetService<IManagerProvider>()!;
            await managerProvider.GetManagers();
            IProjectProvider projectProvider = serviceProvider.GetService<IProjectProvider>()!;
            await projectProvider.GetProjects();
            IRoleProvider roleProvider = serviceProvider.GetService<IRoleProvider>()!;
            await roleProvider.GenerateRoleList();
            await displayOptions.DisplayMainMenu();
        }
    }
}