using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Enums;
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
        private readonly IImageHelper _imageHelper;


        public TripsController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper,IImageHelper imageHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
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
                User = userEntity,
                TripDetails = new List<TripDetailsEntity>
                {
                     new TripDetailsEntity
                    {
                        Origin = tripRequest.Origin,
                        Description= tripRequest.Description,

                    }
                }
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

        [HttpPost]
        [Route("GetMyTrips")]
        public async Task<IActionResult> GetMyTrips([FromBody] MyTripsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripEntities = await _context.Trips
                .Where(t => t.User.Id == request.UserId)
                .Include(t => t.User)
                .OrderByDescending(t => t.StartDateTrip)
                .ToListAsync();

            List<TripResponse> tripsList = new List<TripResponse>();
            foreach (TripEntity element in tripEntities)
            {
                tripsList.Add(_converterHelper.ToTripResponse(element));
            }

            return Ok(tripsList);
        }

        [HttpPost]
        [Route("AddNewExpense")]
        public async Task<IActionResult> AddNewExpense([FromBody] ExpensesRequest expensesRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string receiptPath = string.Empty;
            if (expensesRequest.PictureArray != null && expensesRequest.PictureArray.Length > 0)
            {
                receiptPath = _imageHelper.UploadImage(expensesRequest.PictureArray, "Receipts");
            }

            TripDetailsEntity tripDetails = await _context.TripDetails
              .Include(t => t.Expenses)
              .FirstOrDefaultAsync(t => t.Id == expensesRequest.Id);
            if (tripDetails == null)
            {
                return BadRequest("Trip not found.");
            }

            if (tripDetails.Expenses == null)
            {
                tripDetails.Expenses = new List<ExpensesEntity>();
            }


            tripDetails.Expenses.Add(new ExpensesEntity
            {
                Value = expensesRequest.Value,
                ExpenseType = expensesRequest.ExpensesType,
                //CreatedDate = DateTime.UtcNow
                BillPath = receiptPath
            });


            _context.TripDetails.Update(tripDetails);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /*
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] ExpensesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            string receiptPath = string.Empty;
            if (request.PictureArray != null && request.PictureArray.Length > 0)
            {
                receiptPath = _imageHelper.UploadImage(request.PictureArray, "Receipts");
            }

            //Obyener el viaje con id request.travelId x
            //Obtener el expense type con id request.xxpenseTypeId x
            List<TripDetailsEntity> auxTravelList = await _context.TripDetails.Where(t => t.Id == request.Id).ToListAsync();
            

            ExpensesEntity expenseEntity = new ExpensesEntity
            {
                Value = request.Value,   
                Id = request.Id,
                TripDetails = auxTravelList[0],
                ExpenseType = request.ExpensesType,
                BillPath = receiptPath
            };

            _context.Expenses.Add(expenseEntity);
            await _context.SaveChangesAsync();

            return Ok(new Response
            {
                IsSuccess = true,
                Message = "Gasto guardado existosamente."
            });
        }
        */
    }
}