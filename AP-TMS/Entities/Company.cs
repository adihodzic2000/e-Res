using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Company : BaseEntity
    {
        public string? Title { get; set; }
        public string? Address { get; set; }

        [ForeignKey(nameof(Logo))]
        public Guid LogoId { get; set; }
        public Images? Logo { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location? Location { get; set; }
        public Guid LocationId { get; set; }
    }
}
