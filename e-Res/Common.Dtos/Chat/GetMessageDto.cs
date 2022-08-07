using Common.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Chat
{
    public class GetMessageDto
    {
        public Guid UserFromId { get; set; }
        public Guid UserToId { get; set; }
        public UserGetDto UserFrom { get; set; }
        public UserGetDto UserTo { get; set; }
      
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
