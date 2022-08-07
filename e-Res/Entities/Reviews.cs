using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reviews : BaseEntity
    {
        public double Grade { get; set; }
        
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
        public Guid CompanyId { get; set; }

    }
}
