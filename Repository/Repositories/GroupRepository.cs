using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Education)
                .Include(g => g.GroupsStudents).ThenInclude(gs => gs.Student)
                .Include(g => g.Room) 
                .Include(g => g.Teacher)
                .ToListAsync();
        }

        public override async Task<Group> GetById(int id)
        {
            return await _context.Groups
                .Include(g => g.Education)
                .Include(g => g.GroupsStudents).ThenInclude(gs => gs.Student)
                .Include(g => g.Room)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
