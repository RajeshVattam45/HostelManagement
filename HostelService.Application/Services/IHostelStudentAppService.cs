using HostelService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.Services
{
    public interface IHostelStudentAppService
    {
        Task<HostelStudentDto?> GetByIdAsync ( int id );
        Task<IEnumerable<HostelStudentDto>> GetByHostelAsync ( int hostelId );
        Task<HostelStudentDto> AddAsync ( CreateHostelStudentDto dto );
        Task UpdateAsync ( int id, UpdateHostelStudentDto dto );
        Task DeleteAsync ( int id );
    }
}
