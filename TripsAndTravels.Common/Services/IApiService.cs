using System.Threading.Tasks;
using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Common.Services
{
        public interface IApiService
        {
            Task<Response> GetTripAsync(string idtrip, string urlBase, string servicePrefix, string controller);
            Task<bool> CheckConnectionAsync(string url);

    }

}

