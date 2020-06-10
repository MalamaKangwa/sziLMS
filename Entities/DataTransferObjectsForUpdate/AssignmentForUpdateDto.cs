
using Entities.DataTransferObjectsForCreation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjectsForUpdate
{
    public class AssignmentForUpdateDto
    {
        public string Assignment_Name { get; set; }
        public string Assignment_Description { get; set; }

        public IEnumerable<SubmissionForCreationViaAssignmentDto> Submissions { get; set; }
    }
}
