using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TripsAndTravels.Web.Data;
using TripsAndTravels.Web.Data.Entities;
using TripsAndTravels.Web.Helpers;

namespace TripsAndTravels.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        public TripsController(
            DataContext context,
            IConverterHelper converterHelper
            )
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet("{idtrip}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] string idtrip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            idtrip = idtrip.ToUpper();
            TripEntity tripEntity = await _context.Trips
               .Include(t => t.User)
               .Include(t => t.TripDetails)
               .ThenInclude(t => t.Expenses)
               .ThenInclude(t => t.TripDetails)
               .ThenInclude(t => t.Trip)
               .Include(t => t.User)
               .FirstOrDefaultAsync(t => t.IdTrip == idtrip);


            if (tripEntity == null)
            {
                _context.Trips.Add(tripEntity);
                await _context.SaveChangesAsync();
                return Ok(_converterHelper.ToTripResponse(tripEntity));
            }

            return Ok(_converterHelper.ToTripResponse(tripEntity));

        }
    }
}
