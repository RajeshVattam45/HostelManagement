using HostelService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelService.Domain.Interfaces;
using HostelService.Domain.Entities;
using HostelService.Application.DTOs;


namespace HostelService.Application.Services
{
    public class HostelAppService : IHostelAppService
    {
        private readonly IHostelRepository _repo;

        public HostelAppService ( IHostelRepository repo )
        {
            _repo = repo;
        }


        public async Task<HostelDto?> GetByIdAsync ( int id )
        {
            var entity = await _repo.GetByIdAsync ( id );
            if (entity == null) return null;

            return MapToDto ( entity );
        }

        public async Task<IEnumerable<HostelDto>> GetAllAsync ( )
        {
            var hostels = await _repo.GetAllAsync ();
            return hostels.Select ( MapToDto );
        }

        public async Task<HostelDto> CreateAsync ( CreateHostelDto dto )
        {
            var hostel = new Hostel
            {
                HostelName = dto.HostelName,
                HostelType = dto.HostelType,
                Description = dto.Description,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                Pincode = dto.Pincode,
                TotalFloors = dto.TotalFloors,
                WardenName = dto.WardenName,
                ContactNumber = dto.ContactNumber,
            };

            var added = await _repo.AddAsync ( hostel );
            return MapToDto ( added );
        }

        public async Task UpdateAsync ( UpdateHostelDto dto )
        {
            var entity = await _repo.GetByIdAsync ( dto.Id );
            if (entity == null)
                throw new Exception ( "Hostel not found" );

            // patch update
            entity.HostelName = dto.HostelName ?? entity.HostelName;
            entity.HostelType = dto.HostelType ?? entity.HostelType;
            entity.Description = dto.Description ?? entity.Description;

            entity.AddressLine1 = dto.AddressLine1 ?? entity.AddressLine1;
            entity.AddressLine2 = dto.AddressLine2 ?? entity.AddressLine2;
            entity.City = dto.City ?? entity.City;
            entity.State = dto.State ?? entity.State;
            entity.Pincode = dto.Pincode ?? entity.Pincode;

            entity.TotalFloors = dto.TotalFloors ?? entity.TotalFloors;

            entity.WardenName = dto.WardenName ?? entity.WardenName;
            entity.ContactNumber = dto.ContactNumber ?? entity.ContactNumber;

            if (dto.IsActive.HasValue)
                entity.IsActive = dto.IsActive.Value;

            entity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync ( entity );
        }

        public async Task DeleteAsync ( int id )
        {
            await _repo.DeleteAsync ( id );
        }

        private HostelDto MapToDto ( Hostel h )
        {
            return new HostelDto
            {
                Id = h.Id,
                HostelName = h.HostelName,
                HostelType = h.HostelType,
                City = h.City,
                ContactNumber = h.ContactNumber,
                IsActive = h.IsActive
            };
        }
    }
}
