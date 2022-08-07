
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Room : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
        public Guid CompanyId { get; set; }
    }
}
