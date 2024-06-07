using EmployeeDirectory.BAL.Interfaces;
using EmployeeDirectory.DAL.Interfaces;


namespace EmployeeDirectory.BAL.Providers
{
    public class LocationProvider(ILocationRepository LocationRepository) : ILocationProvider
    {
        public static Dictionary<int, string> Location = new();
        private readonly ILocationRepository _LocationRepository = LocationRepository;

        public async Task GetLocations()
        {
            List<DAL.Models.Location> locations = await _LocationRepository.GetAll();
            foreach (DAL.Models.Location location in locations)
            {
               Location.Add(location.Id, location.Name);
            }
        }
    }
}
