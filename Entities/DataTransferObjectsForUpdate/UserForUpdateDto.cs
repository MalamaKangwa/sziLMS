using System;
using System.Collections.Generic;
using System.Text;
using Entities.DataTransferObjectsForCreation;

namespace Entities.DataTransferObjectsForUpdate
{
    public class UserForUpdateDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<EnrollmentForCreationViaUserDto> Enrollments { get; set; }
    }
}
