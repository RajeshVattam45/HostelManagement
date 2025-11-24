using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class CreateRoomDto
    {
        public int HostelId { get; set; }
        public string RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public string RoomType { get; set; }
        public int TotalBeds { get; set; }
        public int OccupiedBeds { get; set; }
        public decimal FeePerBed { get; set; }
    }
}
