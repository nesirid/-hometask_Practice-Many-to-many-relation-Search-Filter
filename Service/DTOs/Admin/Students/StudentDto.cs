using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<GroupDto> Groups { get; set; }
        public IEnumerable<string> Educations { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
