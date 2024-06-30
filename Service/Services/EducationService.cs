using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Educations;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepo;
        private readonly IMapper _mapper;

        public EducationService(IEducationRepository educationRepo, IMapper mapper)
        {
            _educationRepo = educationRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(EducationCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var education = _mapper.Map<Education>(model);
            await _educationRepo.CreateAsync(education);
        }

        public async Task EditAsync(int id, EducationEditDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingEducation = await _educationRepo.GetById(id);
            if (existingEducation == null) throw new KeyNotFoundException("Education not found");

            _mapper.Map(model, existingEducation);
            await _educationRepo.EditAsync(id, existingEducation);
        }

        public async Task DeleteAsync(int id)
        {
            var education = await _educationRepo.GetById(id);
            if (education == null) throw new KeyNotFoundException("Education not found");
            await _educationRepo.DeleteAsync(education);
        }

        public async Task<IEnumerable<EducationDto>> GetAllAsync()
        {
            var educations = await _educationRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<EducationDto>>(educations);
        }

        public async Task<EducationDto> GetByIdAsync(int id)
        {
            var education = await _educationRepo.GetById(id);
            if (education == null) throw new KeyNotFoundException("Education not found");
            return _mapper.Map<EducationDto>(education);
        }
    }
}
