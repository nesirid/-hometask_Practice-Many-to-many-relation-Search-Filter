using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Rooms;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepo, IMapper mapper)
        {
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(RoomCreateDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            await _roomRepo.CreateAsync(_mapper.Map<Room>(model));
        }

        public async Task EditAsync(int id, RoomEditDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var existingRoom = await _roomRepo.GetById(id);
            if (existingRoom == null) throw new KeyNotFoundException("Room not found");

            _mapper.Map(model, existingRoom);
            await _roomRepo.EditAsync(id, existingRoom);
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _roomRepo.GetById(id);
            if (room == null) throw new KeyNotFoundException("Room not found");
            await _roomRepo.DeleteAsync(room);
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepo.GetAllAsync();
            var roomDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            foreach (var roomDto in roomDtos)
            {
                var room = rooms.FirstOrDefault(r => r.Id == roomDto.Id);
                if (room != null && room.Groups != null)
                {
                    roomDto.Groups = room.Groups.Select(g => g.Name).ToList();
                }
            }

            return roomDtos;
        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            var room = await _roomRepo.GetById(id);
            if (room == null) throw new KeyNotFoundException("Room not found");

            var roomDto = _mapper.Map<RoomDto>(room);
            roomDto.Groups = room.Groups.Select(g => g.Name).ToList();
            return roomDto;
        }
    }
}
