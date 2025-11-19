using HostelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetByIdAsync ( int id );
        Task<IEnumerable<Room>> GetAllByHostelAsync ( int hostelId );
        Task<Room> AddAsync ( Room room );
        Task UpdateAsync ( Room room );
        Task DeleteAsync ( int id );
    }
}
