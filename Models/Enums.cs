﻿namespace dashboardManger.Models
{
    public enum UserRole
    {
        Admin = 1,
        Normal_User = 2  
    }

    public enum State
    {
        All = 0,
        Active = 1,
        Inactive = 2
    }

    public enum Department
    {
        Admin = 1,
        Sales = 2,
        Marketing = 3,
        Rental = 4
    }

    public enum ProperyStatus
    {
        Listing = 1,
        Withdrawn = 2,
        Sold = 3
    }

    public enum PropertyType
    {
        House = 1,
        Apartment = 2,
        Townhouse = 3,
        Land = 4,
        Rural = 5,
        Commercial = 6
    }

}
