using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjectsForCreation
{
    public class UserForCreationDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<EnrollmentForCreationViaUserDto> Enrollments { get; set; }
    }
}
