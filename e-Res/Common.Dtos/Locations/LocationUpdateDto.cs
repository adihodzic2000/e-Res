using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Locations
{
    public class LocationUpdateDto
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Guid CityId { get; set; }
       
    }
}
