using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Students;
using Service.Services.Interface;

namespace Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IGroupRepository _groupRepo;
        private readonly IEducationRepository _educationRepo;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepo,
                              IGroupRepository groupRepo,
                              IEducationRepository educationRepo,
                              IMapper mapper)
        {
            _studentRepo = studentRepo;
            _groupRepo = groupRepo;
            _educationRepo = educationRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(StudentCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var group = await _groupRepo.GetById(model.GroupId);
            if (group == null) throw new KeyNotFoundException("Group not found");

            var education = await _educationRepo.GetById(model.EducationId);
            if (education == null) throw new KeyNotFoundException("Education not found");

            var student = _mapper.Map<Student>(model);

            student.GroupsStudents = new List<GroupsStudents>
            {
                new GroupsStudents { GroupId = model.GroupId, Student = student }
            };

            student.Educations = new List<Education> { education };

            await _studentRepo.CreateAsync(student);
        }

        public async Task EditAsync(int id, StudentEditDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingStudent = await _studentRepo.GetById(id);
            if (existingStudent == null) throw new KeyNotFoundException("Student not found");

            var education = await _educationRepo.GetById(model.EducationId);
            if (education == null) throw new KeyNotFoundException("Education not found");

            _mapper.Map(model, existingStudent);

            if (existingStudent.Educations == null)
            {
                existingStudent.Educations = new List<Education> { education };
            }
            else
            {
                existingStudent.Educations.Clear();
                existingStudent.Educations.Add(education);
            }
            await _studentRepo.EditAsync(id, existingStudent);
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _studentRepo.GetById(id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            await _studentRepo.DeleteAsync(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepo.GetAllAsync();
            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            foreach (var studentDto in studentDtos)
            {
                var student = students.FirstOrDefault(s => s.Id == studentDto.Id);
                if (student != null)
                {
                    if (student.GroupsStudents != null)
                    {
                        studentDto.Groups = _mapper.Map<List<GroupDto>>(student.GroupsStudents.Select(gs => gs.Group).ToList());
                    }
                    if (student.Educations != null)
                    {
                        studentDto.Educations = student.Educations.Select(e => e.Name).ToList();
                    }
                }
            }

            return studentDtos;
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _studentRepo.GetById(id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            var studentDto = _mapper.Map<StudentDto>(student);
            if (student.GroupsStudents != null)
            {
                studentDto.Groups = _mapper.Map<List<GroupDto>>(student.GroupsStudents.Select(gs => gs.Group).ToList());
            }
            if (student.Educations != null)
            {
                studentDto.Educations = student.Educations.Select(e => e.Name).ToList();
            }
            return studentDto;
        }

        public async Task<IEnumerable<StudentDto>> FilterAsync(string name, string surname, int? age)
        {
            var students = await _studentRepo.GetAllAsync();
            var filteredStudents = students.Where(s => (name == null || s.Name.Contains(name)) &&
                                                       (surname == null || s.Surname.Contains(surname)) &&
                                                       (!age.HasValue || s.Age == age)).ToList();
            return _mapper.Map<IEnumerable<StudentDto>>(filteredStudents);
        }

        public async Task<IEnumerable<StudentDto>> SearchAsync(string name)
        {
            var students = await _studentRepo.GetAllAsync();
            var filteredStudents = students.Where(s => s.Name.Contains(name)).ToList();
            return _mapper.Map<IEnumerable<StudentDto>>(filteredStudents);
        }

        public async Task<IEnumerable<StudentDto>> SortAsync(string sortBy)
        {
            var students = await _studentRepo.GetAllAsync();
            switch (sortBy)
            {
                case "Name":
                    students = students.OrderBy(s => s.Name).ToList();
                    break;
                case "Surname":
                    students = students.OrderBy(s => s.Surname).ToList();
                    break;
                case "Age":
                    students = students.OrderBy(s => s.Age).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
    }
}
