using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class HostelDto
    {
        public int Id { get; set; }
        public string HostelName { get; set; }
        public string HostelType { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
