using System;
using System.Collections.Generic;
using System.Text;
using Entities.DataTransferObjectsForCreation;

namespace Entities.DataTransferObjectsForCreation
{
    public class AssignmentForCreationDto
    {
        public string Assignment_Name { get; set; }
        public string Assignment_Description { get; set; }

        public IEnumerable<SubmissionForCreationViaAssignmentDto> Submissions { get; set; }
    }
}

