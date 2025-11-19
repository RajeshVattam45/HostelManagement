using HostelService.Domain.Entities;
using HostelService.Domain.Interfaces;
using HostelService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HostelDbContext _db;
        public RoomRepository ( HostelDbContext db ) => _db = db;

        public async Task<Room> AddAsync ( Room room )
        {
            await _db.Rooms.AddAsync ( room );
            await _db.SaveChangesAsync ();
            return room;
        }

        public async Task DeleteAsync ( int id )
        {
            var r = await _db.Rooms.FindAsync ( id );
            if (r == null) return;
            r.IsActive = false;
            r.UpdatedAt = DateTime.UtcNow;
            _db.Rooms.Update ( r );
            await _db.SaveChangesAsync ();
        }

        public async Task<IEnumerable<Room>> GetAllByHostelAsync ( int hostelId )
        {
            return await _db.Rooms
                            .AsNoTracking ()
                            .Where ( r => r.HostelId == hostelId )
                            .OrderBy ( r => r.FloorNumber )
                            .ToListAsync ();
        }

        public async Task<Room?> GetByIdAsync ( int id )
        {
            return await _db.Rooms.AsNoTracking ().FirstOrDefaultAsync ( r => r.Id == id );
        }

        public async Task UpdateAsync ( Room room )
        {
            room.UpdatedAt = DateTime.UtcNow;
            _db.Rooms.Update ( room );
            await _db.SaveChangesAsync ();
        }
    }
}
