
using Service.DTOs.Admin.Students;

namespace Service.DTOs.Admin.Groups
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<StudentDto> Students { get; set; }
        
        //Room
        public int RoomId { get; set; }
        public string RoomName { get; set; }

        // Education
        public int EducationId { get; set; }
        public string EducationName { get; set; }

        // Teacher
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public DateTime CreatedDate  { get; set; } = DateTime.UtcNow;
    }
}
