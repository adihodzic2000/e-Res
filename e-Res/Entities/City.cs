using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class City : BaseEntity
    {
        public string Title { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
    }
}
