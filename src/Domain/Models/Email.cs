namespace Domain.Models
{
    public class Email
    {
        public int EmailTemplateId { get; set; }

        public int EmailTypeId { get; set; }

        public string EmailTypeName { get; set; }        

        public string Subject { get; set; }

        public string Body { get; set; }

        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string ToAddress { get; set; }
        public string CCAddress { get; set; }

        public bool Active { get; set; }
    }
}
