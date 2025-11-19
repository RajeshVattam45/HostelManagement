using HostelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Interfaces
{
    public interface IHostelRepository
    {
        Task<Hostel?> GetByIdAsync ( int id );
        Task<IEnumerable<Hostel>> GetAllAsync ( );
        Task<Hostel> AddAsync ( Hostel hostel );
        Task UpdateAsync ( Hostel hostel );
        Task DeleteAsync ( int id );
        Task<int> CountAsync ( string? search = null );

        // Advanced
        Task<IEnumerable<Hostel>> GetWithRoomsAsync ( int hostelId );
    }
}
