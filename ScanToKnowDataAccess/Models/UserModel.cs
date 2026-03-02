using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("contact_number")]
        public string? ContactNumber { get; set; }

        [Column("department")]
        public string? Department { get; set; }

        [Column("position")]
        public string? Position { get; set; }

        [Column("profile_picture")]
        public string? ProfilePicture { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}