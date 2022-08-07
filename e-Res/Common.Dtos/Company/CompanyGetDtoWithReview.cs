using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto.Images;
using Common.Dto.Locations;
using Entities;
namespace Common.Dto.Company
{
    public class CompanyGetDtoWithReview
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public ImageGetDto Logo { get; set; }
        public LocationGetDto Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApartment { get; set; }
        public bool IsHotel { get; set; }
        public double AvgReview { get; set; } = 5;
    }
}
