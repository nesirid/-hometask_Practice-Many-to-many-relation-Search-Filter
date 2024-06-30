using Service.DTOs.Admin.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task CreateAsync(RoomCreateDto model);
        Task EditAsync(int id, RoomEditDto model);
        Task DeleteAsync(int id);
        Task<RoomDto> GetByIdAsync(int id);
    }
}
