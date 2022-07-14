﻿using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}