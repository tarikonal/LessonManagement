using AutoMapper;
using Domain.Entities;
using Domain.DTOs.Lesson;
using Domain.DTOs.Family;
using Domain.DTOs.Student;
using Domain.DTOs.Teacher;
using Domain.DTOs.Session;


namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();

            CreateMap<CreateFamilyDto, Family>();
            CreateMap<Family, FamilyDto>();
            CreateMap<UpdateFamilyDto, Family>();

            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();

            CreateMap<Teacher, TeacherDto>();
            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();

            CreateMap<Session, SessionDto>();
            CreateMap<CreateSessionDto, Session>();
            CreateMap<UpdateSessionDto, Session>();

        }
    }
}
