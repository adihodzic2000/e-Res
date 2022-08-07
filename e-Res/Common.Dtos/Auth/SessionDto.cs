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
        public long TokenExpireDate { get; set; }
    }
}
