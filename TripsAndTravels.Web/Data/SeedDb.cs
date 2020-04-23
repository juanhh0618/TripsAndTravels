using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripsAndTravels.Common.Enums;
using TripsAndTravels.Web.Data.Entities;
using TripsAndTravels.Web.Helpers;

namespace TripsAndTravels.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Hernandez", "juancho061899@gmail.com", "350 634 2747", "ruta.jpg", UserType.Admin);
            var employee0 = await CheckUserAsync("2020", "Juan", "Hernandez", "juan061899@gmail.com", "350 634 2747", "ruta.jpg", UserType.Employee);
            var employee1 = await CheckUserAsync("3030", "Juan", "Hernandez", "jchh061899@gmail.com", "350 634 2747", "ruta.jpg", UserType.Employee);
            var employee2 = await CheckUserAsync("4040", "Juan", "Hernandez", "juanhernandez236636@correo.itm.edu.co", "350 634 2747", "ruta.jpg", UserType.Employee);
            await CheckTripsAsync(employee0, employee1, employee2);
        }

        private async Task<UserEntity> CheckUserAsync(
             string document,
             string firstName,
             string lastName,
             string email,
             string phone,
             string picturepath,
             UserType userType)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    PicturePath = picturepath,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Employee.ToString());
        }


        private async Task CheckTripsAsync
            (
                UserEntity employee0,
                UserEntity employee1,
                UserEntity employee2
            )
        {
            if (_dataContext.Trips.Any())
            {
                return;
            }

            _dataContext.Trips.Add(new TripEntity
            {
                User = employee0,
                IdTrip = "MED001",
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Medellin",
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                        Origin = "Bogota",
                        
                        Description = "Tuve que viajar por una reunión de un negocio de la empresa",
                        
                        Expenses = new List<ExpensesEntity>
                        {
                            new ExpensesEntity
                            {
                                Value = 150000 ,
                                ExpenseType = "Transport" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 200000 ,
                                ExpenseType = "Food" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 300000 ,
                                ExpenseType = "Hotel" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 25000 ,
                                ExpenseType = "Taxes" ,
                                BillPath = "bill.png"
                            }
                        }
                    }
                }
            });

            _dataContext.Trips.Add(new TripEntity
            {
                User = employee1,
                IdTrip = "CTG028",
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Cartagena",
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                        Origin = "Medellin",
                        
                        Description = "Vacaciones pagas por la empresa",
                        
                        Expenses = new List<ExpensesEntity>
                        {
                            new ExpensesEntity
                            {
                                Value = 350000 ,
                                ExpenseType = "Transport" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 400000 ,
                                ExpenseType = "Food" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 500000 ,
                                ExpenseType = "Hotel" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 75000 ,
                                ExpenseType = "Taxes" ,
                                BillPath = "bill.png"
                            }
                        }
                    }
                }
            });
            _dataContext.Trips.Add(new TripEntity
            {
                User = employee2,
                IdTrip = "BOG004",
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Bogota",
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                        Origin = "Neiva",
                       
                        Description = "Por motivo de proximo viaje la empresa me solicito sacar la visa",
                        
                        Expenses = new List<ExpensesEntity>
                        {
                            new ExpensesEntity
                            {
                                Value = 350000 ,
                                ExpenseType = "Transport" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 400000 ,
                                ExpenseType = "Food" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 500000 ,
                                ExpenseType = "Hotel" ,
                                BillPath = "bill.png"
                            },

                            new ExpensesEntity
                            {
                                Value = 500000 ,
                                ExpenseType = "Taxes" ,
                                BillPath = "bill.png"
                            }
                        }
                    }
                }
            });
            await _dataContext.SaveChangesAsync();
        }
    }
}