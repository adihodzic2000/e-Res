using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Reviews
{
    public class ReviewCreateDto
    {
        public double Grade { get; set; }
        public Guid CompanyId { get; set; }
    }
}
