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
    internal class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.GroupsStudents)
                .ThenInclude(gs => gs.Group)
                .ThenInclude(g => g.Education)
                .Include(s => s.Educations)
                .ThenInclude(g => g.Room)
                .ToListAsync();
        }

        public override async Task<Student> GetById(int id)
        {
            return await _context.Students
                .Include(s => s.GroupsStudents)
                .ThenInclude(gs => gs.Group)
                .ThenInclude(g => g.Room)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }

}
