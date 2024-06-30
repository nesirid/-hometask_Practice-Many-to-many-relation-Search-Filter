using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Education>> SearchAsync(string name)
        {
            return await _context.Educations
                .Where(e => e.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Education>> GetAllSortedByNameAsync()
        {
            return await _context.Educations
                .OrderBy(e => e.Name)
                .ToListAsync();
        }
        public async Task<Education> GetById(int id)
        {
            return await _context.Educations.FindAsync(id);
        }
    }
}
