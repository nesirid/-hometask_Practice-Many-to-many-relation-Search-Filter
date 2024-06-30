using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Admin.Students
{
    public class StudentCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int EducationId { get; set; }
    }
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required");
            RuleFor(x => x.Email).NotNull().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be greater than 0");
            RuleFor(x => x.GroupId).GreaterThan(0).WithMessage("GroupId is required and must be greater than 0");
            RuleFor(x => x.EducationId).GreaterThan(0).WithMessage("EducationId is required and must be greater than 0");
        }
    }
}
