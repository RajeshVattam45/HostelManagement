using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.DTOs
{
    public class CreateHostelStudentDto
    {
        public int StudentId { get; set; }
        public int HostelId { get; set; }
        public int RoomId { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
