using FluentValidation;
using System.ComponentModel.DataAnnotations;


namespace Service.DTOs.Admin.Groups
{
    public class GroupCreateDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int? RoomId { get; set; }

        [Required]
        public int EducationId { get; set; }
    }

    public class GroupCreateDtoValidator : AbstractValidator<GroupCreateDto>
    {
        public GroupCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0");
            RuleFor(x => x.RoomId).GreaterThan(0).When(x => x.RoomId.HasValue).WithMessage("RoomId must be greater than 0");
            RuleFor(x => x.EducationId).GreaterThan(0).WithMessage("EducationId is required and must be greater than 0");
        }
    }
}
