using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Students;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepo;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepo;
        private readonly IEducationRepository _educationRepo;
        private readonly ILogger<GroupService> _logger;

        public GroupService(IGroupRepository groupRepo, IRoomRepository roomRepo, IEducationRepository educationRepo, IMapper mapper, ILogger<GroupService> logger)
        {
            _groupRepo = groupRepo;
            _mapper = mapper;
            _roomRepo = roomRepo;
            _educationRepo = educationRepo;
            _logger = logger;
        }

        public async Task CreateAsync(GroupCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var group = _mapper.Map<Group>(model);

            if (model.RoomId.HasValue)
            {
                var room = await _roomRepo.GetById(model.RoomId.Value);
                if (room == null)
                {
                    throw new KeyNotFoundException("Room not found");
                }
                group.RoomId = model.RoomId.Value;
            }

            var education = await _educationRepo.GetById(model.EducationId);
            if (education == null)
            {
                throw new KeyNotFoundException("Education not found");
            }
            group.EducationId = model.EducationId;

            await _groupRepo.CreateAsync(group);
        }

        public async Task EditAsync(int id, GroupEditDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingGroup = await _groupRepo.GetById(id);
            if (existingGroup == null) throw new KeyNotFoundException("Group not found");

            if (model.RoomId.HasValue)
            {
                var room = await _roomRepo.GetById(model.RoomId.Value);
                if (room == null)
                {
                    throw new KeyNotFoundException("Room not found");
                }
                existingGroup.RoomId = model.RoomId.Value;
            }

            var education = await _educationRepo.GetById(model.EducationId);
            if (education == null)
            {
                throw new KeyNotFoundException("Education not found");
            }
            existingGroup.EducationId = model.EducationId;

            _mapper.Map(model, existingGroup);
            await _groupRepo.EditAsync(id, existingGroup);
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _groupRepo.GetById(id);
            if (group == null) throw new KeyNotFoundException("Group not found");
            await _groupRepo.DeleteAsync(group);
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var groups = await _groupRepo.GetAllAsync();
            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);

            foreach (var groupDto in groupDtos)
            {
                var group = groups.FirstOrDefault(g => g.Id == groupDto.Id);
                if (group != null && group.GroupsStudents != null)
                {
                    groupDto.Students = _mapper.Map<List<StudentDto>>(group.GroupsStudents.Select(gs => gs.Student).ToList());
                    if (group.Room != null)
                    {
                        groupDto.RoomId = group.Room.Id;
                        groupDto.RoomName = group.Room.Name;
                    }
                    if (group.Education != null)
                    {
                        groupDto.EducationId = group.Education.Id;
                        groupDto.EducationName = group.Education.Name;
                    }
                }
                else
                {
                    groupDto.Students = new List<StudentDto>();
                }
            }

            return groupDtos;
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _groupRepo.GetById(id);
            if (group == null) throw new KeyNotFoundException("Group not found");
            var groupDto = _mapper.Map<GroupDto>(group);
            groupDto.Students = _mapper.Map<List<StudentDto>>(group.GroupsStudents.Select(gs => gs.Student).ToList());
            if (group.Room != null)
            {
                groupDto.RoomId = group.Room.Id;
                groupDto.RoomName = group.Room.Name;
            }
            if (group.Education != null)
            {
                groupDto.EducationId = group.Education.Id;
                groupDto.EducationName = group.Education.Name;
            }
            return groupDto;
        }

        public async Task<IEnumerable<GroupDto>> FilterAsync(string name, int? capacity)
        {
            var groups = await _groupRepo.GetAllAsync();
            var filteredGroups = groups.Where(g => (name == null || g.Name.Contains(name)) &&
                                                    (!capacity.HasValue || g.Capacity == capacity)).ToList();
            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(filteredGroups);
            foreach (var groupDto in groupDtos)
            {
                var group = filteredGroups.FirstOrDefault(g => g.Id == groupDto.Id);
                if (group != null)
                {
                    groupDto.Students = _mapper.Map<List<StudentDto>>(group.GroupsStudents.Select(gs => gs.Student).ToList());
                    if (group.Room != null)
                    {
                        groupDto.RoomId = group.Room.Id;
                        groupDto.RoomName = group.Room.Name;
                    }
                    if (group.Education != null)
                    {
                        groupDto.EducationId = group.Education.Id;
                        groupDto.EducationName = group.Education.Name;
                    }
                }
            }
            return groupDtos;
        }

        public async Task<IEnumerable<GroupDto>> SearchAsync(string name, string surname, int? age = null)
        {
            var groups = await _groupRepo.GetAllAsync();
            var filteredGroups = groups.Where(g => g.GroupsStudents.Any(gs =>
                            (name == null || gs.Student.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) ||
                            (surname == null || gs.Student.Surname.Contains(surname, StringComparison.OrdinalIgnoreCase)) ||
                            (!age.HasValue || gs.Student.Age == age))).ToList();
            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(filteredGroups);
            foreach (var groupDto in groupDtos)
            {
                var group = filteredGroups.FirstOrDefault(g => g.Id == groupDto.Id);
                if (group != null)
                {
                    groupDto.Students = _mapper.Map<List<StudentDto>>(group.GroupsStudents.Select(gs => gs.Student).ToList());
                    if (group.Room != null)
                    {
                        groupDto.RoomId = group.Room.Id;
                        groupDto.RoomName = group.Room.Name;
                    }
                    if (group.Education != null)
                    {
                        groupDto.EducationId = group.Education.Id;
                        groupDto.EducationName = group.Education.Name;
                    }
                }
            }
            return groupDtos;
        }

        public async Task<IEnumerable<GroupDto>> SortAsync(string sortBy)
        {
            var groups = await _groupRepo.GetAllAsync();
            switch (sortBy)
            {
                case "Name":
                    groups = groups.OrderBy(g => g.Name).ToList();
                    break;
                case "Capacity":
                    groups = groups.OrderBy(g => g.Capacity).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }
            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);
            foreach (var groupDto in groupDtos)
            {
                var group = groups.FirstOrDefault(g => g.Id == groupDto.Id);
                if (group != null)
                {
                    groupDto.Students = _mapper.Map<List<StudentDto>>(group.GroupsStudents.Select(gs => gs.Student).ToList());
                    if (group.Room != null)
                    {
                        groupDto.RoomId = group.Room.Id;
                        groupDto.RoomName = group.Room.Name;
                    }
                    if (group.Education != null)
                    {
                        groupDto.EducationId = group.Education.Id;
                        groupDto.EducationName = group.Education.Name;
                    }
                }
            }
            return groupDtos;
        }
    }
}
