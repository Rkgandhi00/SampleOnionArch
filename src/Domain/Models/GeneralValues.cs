namespace Domain.Models
{
    public class GeneralValues
    {
        public int Id { get; set; }

        public string ValueName { get; set; }

        public int GeneralTypeId { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
