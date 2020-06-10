using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjectsForCreation;
using Entities.DataTransferObjectsForUpdate;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sziLMS
{
    public class MappingProfile: Profile 
    { 
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<Section, SectionDto>();
            CreateMap<Assignment, AssignmentDto>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<Submission, SubmissionDto>();


            CreateMap<UserForCreationDto, User>();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<AssignmentForCreationDto, Assignment>();
            CreateMap<EnrollmentForCreationViaUserDto, Enrollment>();
            CreateMap<EnrollmentForCreationViaSectionDto, Enrollment>();
            CreateMap<SubmissionForCreationViaAssignmentDto, Submission>();
            CreateMap<SubmissionForCreationViaEnrollmentDto, Submission>();

            CreateMap<UserForUpdateDto, User>().ReverseMap();
            CreateMap<CourseForUpdateDto, Course>().ReverseMap();
            CreateMap<AssignmentForUpdateDto, Assignment>();
            CreateMap<EnrollmentForUpdateDto, Enrollment>();
            CreateMap<SubmissionForUpdateDto, Submission>();


            CreateMap<AssignmentDto, AssignmentForCreationDto>();
        }
    }
}
