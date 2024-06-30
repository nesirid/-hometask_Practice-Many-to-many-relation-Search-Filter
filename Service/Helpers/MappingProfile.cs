using AutoMapper;
using Domain.Entities;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Rooms;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;


namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Groups
            CreateMap<GroupCreateDto, Group>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(src => src.EducationId));
            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Name : null))
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(src => src.EducationId))
                .ForMember(dest => dest.EducationName, opt => opt.MapFrom(src => src.Education != null ? src.Education.Name : null));
            CreateMap<GroupEditDto, Group>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.EducationId, opt => opt.MapFrom(src => src.EducationId));


            // Students
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.GroupsStudents.Select(gs => gs.Group)));
            CreateMap<StudentEditDto, Student>();

            // Rooms
            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomEditDto, Room>();
            CreateMap<Room, RoomDto>();

            // Education
            CreateMap<Education, EducationDto>();
            CreateMap<EducationCreateDto, Education>();
            CreateMap<EducationEditDto, Education>();

            // Students
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.GroupsStudents.Select(gs => gs.Group)));
            CreateMap<StudentEditDto, Student>();

            //Teacher
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<Teacher, TeacherDto>();
            CreateMap<TeacherEditDto, Teacher>();
        }
    }
}
