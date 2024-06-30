
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Admin.Groups
{
    public class GroupEditDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        [Required]
        public int RoomId { get; set; }
        [Required]
        public int EducationId { get; set; }
        [Required]
        public int TeacherId { get; set; }
    }
    public class GroupEditDtoValidator : AbstractValidator<GroupEditDto>
    {
        public GroupEditDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0");
            RuleFor(x => x.RoomId).GreaterThan(0).WithMessage("RoomId is required and must be greater than 0");
            RuleFor(x => x.EducationId).GreaterThan(0).WithMessage("EducationId is required and must be greater than 0");
            RuleFor(x => x.TeacherId).GreaterThan(0).WithMessage("TeacherId is required and must be greater than 0");
        }
    }
}
