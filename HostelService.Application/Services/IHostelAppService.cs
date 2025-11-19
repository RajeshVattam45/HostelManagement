using HostelService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.Services
{
    public interface IHostelAppService
    {
        Task<HostelDto?> GetByIdAsync ( int id );
        Task<IEnumerable<HostelDto>> GetAllAsync ( );
        Task<HostelDto> CreateAsync ( CreateHostelDto dto );
        Task UpdateAsync ( UpdateHostelDto dto );
        Task DeleteAsync ( int id );
    }
}
