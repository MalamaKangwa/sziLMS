using Entities.DataTransferObjectsForCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjectsForUpdate
{
    public class CourseForUpdateDto
    {
        public string Course_Name { get; set; }
        public string Course_Description { get; set; }

        public IEnumerable<AssignmentForCreationDto> Assignments { get; set; }
    }
}
