using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public int HostelId { get; set; }
        public Hostel Hostel { get; set; }

        public string RoomNumber { get; set; }           // e.g., A-101
        public int FloorNumber { get; set; }

        public string RoomType { get; set; }             // Standard/AC/Deluxe

        public int TotalBeds { get; set; }
        public int OccupiedBeds { get; set; }

        public decimal FeePerBed { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int AvailableBeds => TotalBeds - OccupiedBeds;
    }

}
