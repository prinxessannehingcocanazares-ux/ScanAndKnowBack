namespace ScanToKnowDataAccess.Dto
{
    public class XUserDto
    {
        public int? XUserId { get; set; }
        public int? XUserUserId { get; set; }
        public string? XUserUserName { get; set; }
        public string? XUserUserEmail { get; set; }

        public string? XUserUserPassword { get; set; }
        public string? XUserDepartmentId { get; set; }
        public string? XUserPositionId { get; set; }

        public string? XUserUserOtp { get; set; }

        public DateTime? XUserOtpExpiry { get; set; }
        public bool XUserStatus { get; set; }
        public string? XUserRole { get; set; }
    }
}