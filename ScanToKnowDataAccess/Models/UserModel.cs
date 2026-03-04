using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        [PrimaryKey("user_id")]
        public int? UserId { get; set; }

        [Column("user_first_name")]
        public string? UserFirstName { get; set; }
        [Column("user_last_name")]
        public string? UserLastName { get; set; }

        [Column("user_email")]
        public string? UserEmail { get; set; }

        [Column("user_contact_number")]
        public string? UserContactNumber { get; set; }

        [Column("user_department")]
        public string? UserDepartment { get; set; }

        [Column("user_position")]
        public string? UserPosition { get; set; }

        [Column("user_profile_picture")]
        public string? UserProfilePicture { get; set; }

        [Column("user_created_at")]
        public DateTime? UserCreatedAt { get; set; }
    }
}