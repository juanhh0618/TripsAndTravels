using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Enums;
using TripsAndTravels.Web.Data.Entities;

namespace TripsAndTravels.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckTripsAsync();
        }

        private async Task CheckTripsAsync()
        {
            if (_dataContext.Trips.Any())
            {
                return;
            }

            _dataContext.Trips.Add(new TripEntity
            {
                IdTrip = "MED001" ,
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Medellin",
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                        Origin = "Bogota",
                        TripType = TripType.Work ,
                        Description = "Tuve que viajar por una reunión de un negocio de la empresa",
                        BillPath = "bill.location.jpg" ,
                        Expenses = new List<ExpensesEntity>
                        {
                            new ExpensesEntity
                            {
                                Value = 150000 ,
                                ExpenseType = ExpenseType.Transport ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 200000 ,
                                ExpenseType = ExpenseType.Food ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 300000 ,
                                ExpenseType = ExpenseType.Hotel ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 25000 ,
                                ExpenseType = ExpenseType.Taxes ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            }
                        }
                    }
                }
            });

            _dataContext.Trips.Add(new TripEntity
            {
                IdTrip = "CTG028" ,
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Cartagena",
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                        Origin = "Medellin",
                        TripType = TripType.Vacations ,
                        Description = "Vacaciones pagas por la empresa",
                        BillPath = "bill.location.jpg" ,
                        Expenses = new List<ExpensesEntity>
                        {
                            new ExpensesEntity
                            {
                                Value = 350000 ,
                                ExpenseType = ExpenseType.Transport ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 400000 ,
                                ExpenseType = ExpenseType.Food ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 500000 ,
                                ExpenseType = ExpenseType.Hotel ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            },

                            new ExpensesEntity
                            {
                                Value = 75000 ,
                                ExpenseType = ExpenseType.Taxes ,
                                ExpenseDate = DateTime.UtcNow.AddMinutes(90)
                            }
                        }
                    }
                }
            });
            await _dataContext.SaveChangesAsync();
        }
    }
}
