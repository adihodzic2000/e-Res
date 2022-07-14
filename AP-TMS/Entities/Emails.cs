﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Emails : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public Images? User { get; set; }

    }
}