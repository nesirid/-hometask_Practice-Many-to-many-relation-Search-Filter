using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task CreateAsync(TeacherCreateDto model);
        Task EditAsync(int id, TeacherEditDto model);
        Task DeleteAsync(int id);
        Task<TeacherDto> GetByIdAsync(int id);
        Task<IEnumerable<TeacherDto>> SearchAsync(string name);
        Task<IEnumerable<TeacherDto>> SortAsync(string sortBy);

    }
}
