﻿using TripsAndTravels.Common.Enums;

namespace TripsAndTravels.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PicturePath { get; set; }

        public string PhoneNumber { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
        ? "https://tripsandtravelswebjc.azurewebsites.net//images/noimage.png"
        : $"https://tripsandtravelswebjc.azurewebsites.net{PicturePath.Substring(1)}";

    }
}
