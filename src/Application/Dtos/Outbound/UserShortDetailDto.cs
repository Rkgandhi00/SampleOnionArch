using Common;
using Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Outbound
{
    public class UserShortDetailDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public int LoginType { get; set; }
        public string EmailAddress { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
