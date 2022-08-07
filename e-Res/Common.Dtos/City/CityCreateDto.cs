using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.City
{
    public class CityCreateDto
    {
        public string Title { get; set; }
        public Guid CountryId { get; set; }

    }
}
