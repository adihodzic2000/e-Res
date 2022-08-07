using Common.Dto.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.City
{
    public class CityGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public CountryGetDto Country  { get; set; }
    }
}
