using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Chat
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(UserFrom))]
        public Guid UserFromId { get; set; }
        public User UserFrom { get; set; }

        [ForeignKey(nameof(UserTo))]
        public Guid UserToId { get; set; }
        public User UserTo { get; set; }
    
        public string? Content { get; set; }
        public bool IsDeleted { get; set; }
        public bool Seen { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
