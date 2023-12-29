namespace Common.Dto.Contracts
{
    public class LocationDto
    {
        public int TotalCount { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public List<location_locationTypeDto> LocationTypeIds { get; set; }
        public List<ClassAndSeatingCapacity>? ClassAndSeatingCapacity { get; set; }
        public string? Address { get; set; }

        public int City { get; set; }

        public int State { get; set; }
        public int ClientCountryId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public string? PinCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public Guid? CRMId { get; set; }

        public bool Active { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
    public class location_locationTypeDto
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public int LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }

    }
    public class ClassAndSeatingCapacity
    {
        public string ClassName { get; set; }
        public int SeatingCapacity { get; set; }

    }
}
