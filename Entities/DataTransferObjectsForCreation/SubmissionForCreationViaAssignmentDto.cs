using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjectsForCreation
{
    public class SubmissionForCreationViaAssignmentDto
    {
        public Guid EnrollmentId { get; set; }
        public string Score { get; set; }
    }
}
