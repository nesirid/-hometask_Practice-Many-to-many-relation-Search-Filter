using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Educations
{
    public class EducationEditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
