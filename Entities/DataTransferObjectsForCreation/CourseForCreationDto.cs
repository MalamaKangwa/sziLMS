using System;
using System.Collections.Generic;
using System.Text;
using Entities.DataTransferObjectsForCreation;

namespace Entities.DataTransferObjectsForCreation
{
    public class CourseForCreationDto
    {
        public string Course_Name { get; set; }
        public string Course_Description { get; set; }

        public IEnumerable<AssignmentForCreationDto> Assignments { get; set; }
    }
}
