using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("x_user")]
    public class XUserModel : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("user_name")]
        public string? UserName { get; set; }
        [Column("user_email")]
        public string? UserEmail { get; set; }

        [Column("user_password")]
        public string? UserPassword { get; set; }

        [Column("user_otp")]
        public string? UserOtp { get; set; }

        [Column("user_role")]
        public string? Role { get; set; }

        [Column("otp_expiry")]
        public DateTime? OtpExpiry { get; set; }


    }
}