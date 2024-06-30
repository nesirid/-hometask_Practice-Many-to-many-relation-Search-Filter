using Domain.Entities;
using Service.DTOs.Admin.Educations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IEducationService
    {
        Task<IEnumerable<EducationDto>> GetAllAsync();
        Task<EducationDto> GetByIdAsync(int id);
        Task CreateAsync(EducationCreateDto model);
        Task EditAsync(int id, EducationEditDto model);
        Task DeleteAsync(int id);
        Task<IEnumerable<EducationDto>> SearchAsync(string name);
        Task<IEnumerable<EducationDto>> GetAllSortedByNameAsync();
    }
}
