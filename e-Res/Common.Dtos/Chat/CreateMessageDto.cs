using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Chat
{
    public class CreateMessageDto
    {
        public Guid UserFromId { get; set; }
        public Guid UserToId { get; set; }
        public string Content { get; set; }
    }
}
