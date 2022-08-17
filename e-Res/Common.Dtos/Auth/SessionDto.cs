using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Auth
{
    public class SessionDto
    {
        public Guid UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long TokenExpireDate { get; set; }
        public Gender Gender { get; set; }
        public List<string> Roles { get; set; }
        public string ProfileImagePath { get; set; }
    }
}
