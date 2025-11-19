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
    public class HostelStudentRepository : IHostelStudentRepository
    {
        private readonly HostelDbContext _db;
        public HostelStudentRepository ( HostelDbContext db ) => _db = db;

        public async Task<HostelStudent> AddAsync ( HostelStudent hs )
        {
            await _db.HostelStudents.AddAsync ( hs );
            await _db.SaveChangesAsync ();
            return hs;
        }

        public async Task DeleteAsync ( int id )
        {
            var e = await _db.HostelStudents.FindAsync ( id );
            if (e == null) return;
            e.IsActive = false;
            _db.HostelStudents.Update ( e );
            await _db.SaveChangesAsync ();
        }

        public async Task<HostelStudent?> GetByIdAsync ( int id )
        {
            return await _db.HostelStudents
                            .AsNoTracking ()
                            .Include ( hs => hs.Hostel )
                            .Include ( hs => hs.Room )
                            .FirstOrDefaultAsync ( hs => hs.Id == id );
        }

        public async Task<IEnumerable<HostelStudent>> GetByHostelAsync ( int hostelId )
        {
            return await _db.HostelStudents
                            .AsNoTracking ()
                            .Where ( hs => hs.HostelId == hostelId )
                            .OrderByDescending ( hs => hs.JoinDate )
                            .ToListAsync ();
        }

        public async Task UpdateAsync ( HostelStudent hs )
        {
            hs.IsActive = true;
            _db.HostelStudents.Update ( hs );
            await _db.SaveChangesAsync ();
        }
    }
}
