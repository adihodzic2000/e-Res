using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Company
{
    public class CompanyUpdateDto
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public Guid? LogoId { get; set; }
        //public Guid? LocationId { get; set; }
     
        //public byte[] ImageURL { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Guid CityId { get; set; }
    }
}
