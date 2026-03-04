using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("positions")]
    public class PositionModel : BaseModel
    {
        [PrimaryKey("position_id")]
        public int PositionId { get; set; }

        [Column("position_title")]
        public string? PositionTitle { get; set; }
    }
}