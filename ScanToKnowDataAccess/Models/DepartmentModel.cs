using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("departments")]
    public class DepartmentModel : BaseModel
    {
        [PrimaryKey("department_id")]
        public int DepartmentId { get; set; }

        [Column("department_college_name")]
        public string? DepartmentCollegeName { get; set; }
    }
}