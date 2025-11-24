using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class UpdateHostelStudentDto
    {
        public int RoomId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
