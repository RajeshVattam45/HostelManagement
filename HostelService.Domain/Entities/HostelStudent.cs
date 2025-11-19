using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Entities
{
    public class HostelStudent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }        // From School DB
        public int HostelId { get; set; }
        public int RoomId { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }

        public bool IsActive { get; set; } = true;

        public Hostel Hostel { get; set; }
        public Room Room { get; set; }
    }

}
