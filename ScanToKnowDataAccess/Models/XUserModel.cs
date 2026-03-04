using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("xUsers")]
    public class XUserModel : BaseModel
    {
        [PrimaryKey("xUser_id")]
        public int XUserId { get; set; }
        [Column("xUser_user_id")]
        public int? XUserUserId { get; set; }
        [Column("xUser_userName")]
        public string? XUserUserName { get; set; }
        [Column("xUser_email")]
        public string? XUserUserEmail { get; set; }

        [Column("xUser_password")]
        public string? XUserUserPassword { get; set; }

        [Column("xUser_department_id")]
        public string? XUserDepartmentId { get; set; }
        [Column("xUser_position_id")]
        public string? XUserPositionId { get;set; }

        [Column("xUser_otp")]
        public string? XUserUserOtp { get; set; }

        [Column("xUser_role")]
        public string? XUserRole { get; set; }

        [Column("xUser_otp_expiry")]
        public DateTime? XUserOtpExpiry { get; set; }


    }
}