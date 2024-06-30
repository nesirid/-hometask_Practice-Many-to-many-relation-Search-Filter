
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Admin.Groups
{
    public class GroupEditDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int? RoomId { get; set; }

        [Required]
        public int EducationId { get; set; }
    }
}
