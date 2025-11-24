using HostelService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.Services
{
    public interface IRoomAppService
    {
        Task<RoomDto?> GetByIdAsync ( int id );
        Task<IEnumerable<RoomDto>> GetAllByHostelAsync ( int hostelId );
        Task<RoomDto> AddAsync ( CreateRoomDto dto );
        Task UpdateAsync ( int id, UpdateRoomDto dto );
        Task DeleteAsync ( int id );
    }
}
