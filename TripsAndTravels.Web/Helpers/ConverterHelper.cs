using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Models;
using TripsAndTravels.Web.Data.Entities;

namespace TripsAndTravels.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TripResponse ToTripResponse(TripEntity tripEntity)
        {

            return new TripResponse
            {
                Id = tripEntity.Id,
                DestinyCity = tripEntity.DestinyCity,
                StartDateTrip = tripEntity.StartDateTrip,
                EndDateTrip = tripEntity.EndDateTrip,
                TripDetails = tripEntity.TripDetails?.Select(t => new TripDetailsResponse
                {


                    Id = t.Id,
                    Origin = t.Origin,
                    Description = t.Description,
                    BillPath = t.BillPath,
                    

                    Expenses = t.Expenses?.Select(ex => new ExpensesResponse
                    {

                        Id = ex.Id,
                        Value = ex.Value,
                        ExpensesType = ex.ExpenseType

                    }).ToList(),

                }).ToList(),
                User = ToUserResponse(tripEntity.User)


            };
        }

        private UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {

                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }
    }
}