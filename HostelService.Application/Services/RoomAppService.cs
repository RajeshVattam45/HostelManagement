using HostelService.Application.DTOs;
using HostelService.Domain.Entities;
using HostelService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Application.Services
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IRoomRepository _repo;

        public RoomAppService ( IRoomRepository repo )
        {
            _repo = repo;
        }

        public async Task<RoomDto?> GetByIdAsync ( int id )
        {
            var room = await _repo.GetByIdAsync ( id );
            if (room == null)
                return null;

            return ToDto ( room );
        }

        public async Task<IEnumerable<RoomDto>> GetAllByHostelAsync ( int hostelId )
        {
            var rooms = await _repo.GetAllByHostelAsync ( hostelId );
            return rooms.Select ( r => ToDto ( r ) );
        }

        public async Task<RoomDto> AddAsync ( CreateRoomDto dto )
        {
            var room = new Room
            {
                HostelId = dto.HostelId,
                RoomNumber = dto.RoomNumber,
                FloorNumber = dto.FloorNumber,
                RoomType = dto.RoomType,
                TotalBeds = dto.TotalBeds,
                OccupiedBeds = dto.OccupiedBeds,
                FeePerBed = dto.FeePerBed
            };

            await _repo.AddAsync ( room );
            return ToDto ( room );
        }

        public async Task UpdateAsync ( int id, UpdateRoomDto dto )
        {
            var room = await _repo.GetByIdAsync ( id );
            if (room == null)
                throw new Exception ( "Room not found" );

            room.RoomNumber = dto.RoomNumber;
            room.FloorNumber = dto.FloorNumber;
            room.RoomType = dto.RoomType;
            room.TotalBeds = dto.TotalBeds;
            room.OccupiedBeds = dto.OccupiedBeds;
            room.FeePerBed = dto.FeePerBed;
            room.IsActive = dto.IsActive;

            await _repo.UpdateAsync ( room );
        }

        public async Task DeleteAsync ( int id )
        {
            await _repo.DeleteAsync ( id );
        }

        private RoomDto ToDto ( Room r ) =>
            new RoomDto
            {
                Id = r.Id,
                HostelId = r.HostelId,
                RoomNumber = r.RoomNumber,
                FloorNumber = r.FloorNumber,
                RoomType = r.RoomType,
                TotalBeds = r.TotalBeds,
                OccupiedBeds = r.OccupiedBeds,
                FeePerBed = r.FeePerBed,
                AvailableBeds = r.AvailableBeds,
                IsActive = r.IsActive
            };
    }
}
