using HostelService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Domain.Interfaces
{
    public interface IHostelStudentRepository
    {
        Task<HostelStudent?> GetByIdAsync ( int id );
        Task<IEnumerable<HostelStudent>> GetByHostelAsync ( int hostelId );
        Task<HostelStudent> AddAsync ( HostelStudent hs );
        Task UpdateAsync ( HostelStudent hs );
        Task DeleteAsync ( int id );
    }
}
