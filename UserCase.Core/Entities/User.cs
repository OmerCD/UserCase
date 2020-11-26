using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UserCase.Core.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
    public class UserRole : IdentityRole<int> { }
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Location Geo { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
