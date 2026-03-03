using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("department")]
    public class DepartmentModel : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("college_name")]
        public string? DepartmentName { get; set; }
    }
}