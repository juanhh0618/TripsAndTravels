using TripsAndTravels.Common.Models;

namespace TripsAndTravels.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}
