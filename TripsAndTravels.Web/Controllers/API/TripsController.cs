/*using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Web.Data;
using TripsAndTravels.Web.Data.Entities;
using TripsAndTravels.Web.Helpers;

namespace TripsAndTravels.Web.Controllers.API
{
    
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        
        public TripsController(
            DataContext context,
            IConverterHelper converterHelper,
            IUserHelper userHelper
            )
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
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

        // POST: api/Travels
        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _userHelper.GetUserByEmailAsync(tripRequest.UserId);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            TripEntity tripEntity = new TripEntity
            {
                IdTrip = tripRequest.IdTrip,
                StartDateTrip = tripRequest.StartDateTrip,
                EndDateTrip = tripRequest.EndDateTrip,
                DestinyCity = tripRequest.DestinyCity,
                User = userEntity
            };

            _context.Trips.Add(tripEntity);
            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToTripResponse(tripEntity)); ;
        }
    }
}
*/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Models;
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
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }



        // POST: api/Travels
        [HttpPost]
        public async Task<IActionResult> PostTravelEntity([FromBody] TripRequest tripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _userHelper.GetUserByEmailAsync(tripRequest.UserId);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            TripEntity tripEntity = new TripEntity
            {
                IdTrip = tripRequest.IdTrip,
                StartDateTrip = tripRequest.StartDateTrip,
                EndDateTrip = tripRequest.EndDateTrip,
                DestinyCity = tripRequest.DestinyCity,
                User = userEntity
            };

            _context.Trips.Add(tripEntity);
            await _context.SaveChangesAsync();

            return Ok(_converterHelper.ToTripResponse(tripEntity)); ;
        }

        // GET: api/Travels/5
        [HttpGet("{IdTrip}")]
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
                return NotFound();
            }

            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }
    }
}