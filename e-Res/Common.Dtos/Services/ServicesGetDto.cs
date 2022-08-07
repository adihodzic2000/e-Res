using Common.Dto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Services
{
    public class ServicesGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
