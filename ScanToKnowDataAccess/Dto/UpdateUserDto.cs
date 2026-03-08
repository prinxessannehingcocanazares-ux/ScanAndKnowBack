using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanToKnowDataAccess.Dto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }

        // File from FormData
        public string? ProfilePicture { get; set; }
    }
}
