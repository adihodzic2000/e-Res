using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        
        [ForeignKey(nameof(CreatedByUserId))]
        public User? CreatedByUser { get; set; }
        public Guid CreatedByUserId { get; set; }
        
        [ForeignKey(nameof(ModifiedByUserId))]
        public User? ModifiedByUser { get; set; }
        public Guid ModifiedByUserId { get; set; }
        
    }
}
