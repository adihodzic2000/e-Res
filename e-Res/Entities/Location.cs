using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Location : BaseEntity
    {
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
        public Guid CityId { get; set; }
    }
}
