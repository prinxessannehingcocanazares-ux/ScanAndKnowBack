

namespace ScanToKnowDataAccess.Models
{
    public class XUserDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }

        public string? UserPassword { get; set; }

        public string? UserOtp { get; set; }

        public DateTime? OtpExpiry { get; set; }
        public bool Status { get; set; }
        public string? Role { get; set; }
    }
}