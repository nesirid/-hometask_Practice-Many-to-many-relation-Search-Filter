using Service.DTOs.Admin.Groups;


namespace Service.Services.Interface
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task CreateAsync(GroupCreateDto model);
        Task EditAsync(int id, GroupEditDto model);
        Task DeleteAsync(int id);
        Task<GroupDto> GetByIdAsync(int id);
        Task<IEnumerable<GroupDto>> FilterAsync(string name, int? capacity);
        Task<IEnumerable<GroupDto>> SearchAsync(string name, string surname, int? age = null);
        Task<IEnumerable<GroupDto>> SortAsync(string sortBy);
    }
}
