using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class CreateHostelDto
    {
        public string HostelName { get; set; }
        public string HostelType { get; set; }
        public string Description { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }

        public int TotalFloors { get; set; }

        public string WardenName { get; set; }
        public string ContactNumber { get; set; }
    }
}
