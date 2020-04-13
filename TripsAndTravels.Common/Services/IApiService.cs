using System.Threading.Tasks;
using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Common.Services
{
        public interface IApiService
        {
            Task<Response> GetTripAsync(string idtrip, string urlBase, string servicePrefix, string controller);
            Task<bool> CheckConnectionAsync(string url);
            Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
            Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);
            Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);
    }

}

