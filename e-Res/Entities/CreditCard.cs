using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CreditCard : BaseEntity
    {
        [ForeignKey(nameof(Bill))]
        public Guid BillId { get; set; }
        public Bill Bill{ get; set; }


    }
}
