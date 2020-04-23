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
        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);
        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);
        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);
        Task<Response> AddNewTripAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, TripRequest tripRequest);
        Task<Response> GetMyTrips(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, MyTripsRequest model);

    }

}
