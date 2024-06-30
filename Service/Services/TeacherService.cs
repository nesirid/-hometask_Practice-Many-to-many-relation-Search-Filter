using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepo, IMapper mapper)
        {
            _teacherRepo = teacherRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(TeacherCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            await _teacherRepo.CreateAsync(_mapper.Map<Teacher>(model));
        }

        public async Task EditAsync(int id, TeacherEditDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingTeacher = await _teacherRepo.GetById(id);
            if (existingTeacher == null) throw new KeyNotFoundException("Teacher not found");

            _mapper.Map(model, existingTeacher);
            await _teacherRepo.EditAsync(id, existingTeacher);
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _teacherRepo.GetById(id);
            if (teacher == null) throw new KeyNotFoundException("Teacher not found");
            await _teacherRepo.DeleteAsync(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _teacherRepo.GetAllAsync());
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepo.GetById(id);
            if (teacher == null) throw new KeyNotFoundException("Teacher not found");
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> SearchAsync(string name)
        {
            var teachers = await _teacherRepo.GetAllAsync();
            var filteredTeachers = teachers.Where(t => t.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return _mapper.Map<IEnumerable<TeacherDto>>(filteredTeachers);
        }

        public async Task<IEnumerable<TeacherDto>> SortAsync(string sortBy)
        {
            var teachers = await _teacherRepo.GetAllAsync();
            switch (sortBy)
            {
                case "Name":
                    teachers = teachers.OrderBy(t => t.Name).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }
    }
}
