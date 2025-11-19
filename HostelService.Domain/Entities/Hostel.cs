using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Entities
{
    public class Hostel
    {
        public int Id { get; set; }
        public string HostelName { get; set; }
        public string HostelType { get; set; }          // Boys/Girls/Mixed
        public string Description { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }

        public int TotalFloors { get; set; }

        public string WardenName { get; set; }
        public string ContactNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Room> Rooms { get; set; }
    }

}
