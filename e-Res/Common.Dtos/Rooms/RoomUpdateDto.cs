using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Rooms
{
    public class RoomUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Guid CompanyId { get; set; }
        public string Color { get; set; }

    }
}
