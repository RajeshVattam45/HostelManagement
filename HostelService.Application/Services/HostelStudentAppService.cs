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
    public class HostelStudentAppService : IHostelStudentAppService
    {
        private readonly IHostelStudentRepository _repo;
        private readonly IRoomRepository _roomRepo;  // Optional if you want room validations

        public HostelStudentAppService (
            IHostelStudentRepository repo,
            IRoomRepository roomRepo )
        {
            _repo = repo;
            _roomRepo = roomRepo;
        }

        public async Task<HostelStudentDto?> GetByIdAsync ( int id )
        {
            var hs = await _repo.GetByIdAsync ( id );
            return hs == null ? null : ToDto ( hs );
        }

        public async Task<IEnumerable<HostelStudentDto>> GetByHostelAsync ( int hostelId )
        {
            var list = await _repo.GetByHostelAsync ( hostelId );
            return list.Select ( hs => ToDto ( hs ) );
        }

        public async Task<HostelStudentDto> AddAsync ( CreateHostelStudentDto dto )
        {
            var hs = new HostelStudent
            {
                StudentId = dto.StudentId,
                HostelId = dto.HostelId,
                RoomId = dto.RoomId,
                JoinDate = dto.JoinDate,
                IsActive = true
            };

            await _repo.AddAsync ( hs );
            return ToDto ( hs );
        }

        public async Task UpdateAsync ( int id, UpdateHostelStudentDto dto )
        {
            var hs = await _repo.GetByIdAsync ( id );
            if (hs == null)
                throw new Exception ( "Hostel student record not found" );

            hs.RoomId = dto.RoomId;
            hs.LeaveDate = dto.LeaveDate;
            hs.IsActive = dto.IsActive;

            await _repo.UpdateAsync ( hs );
        }

        public async Task DeleteAsync ( int id )
        {
            await _repo.DeleteAsync ( id );
        }

        private HostelStudentDto ToDto ( HostelStudent hs )
        {
            return new HostelStudentDto
            {
                Id = hs.Id,
                StudentId = hs.StudentId,
                HostelId = hs.HostelId,
                RoomId = hs.RoomId,
                JoinDate = hs.JoinDate,
                LeaveDate = hs.LeaveDate,
                IsActive = hs.IsActive,

                HostelName = hs.Hostel?.HostelName,
                RoomNumber = hs.Room?.RoomNumber
            };
        }
    }
}
