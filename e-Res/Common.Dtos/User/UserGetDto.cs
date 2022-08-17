using Common.Dto.Company;
using Common.Dto.Images;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.User
{
    public class UserGetDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public CompanyGetDto Company { get; set; }
        public ImageGetDto Image { get; set; }
    }
}
