using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class HostelStudentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int HostelId { get; set; }
        public int RoomId { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }

        public bool IsActive { get; set; }

        public string HostelName { get; set; }
        public string RoomNumber { get; set; }
    }
}
