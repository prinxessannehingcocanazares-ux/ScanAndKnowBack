using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanToKnowDataAccess.Models
{
    [Table("rooms")]
    public class RoomModel : BaseModel
    {
        [PrimaryKey("room_id")]
        public int RoomId { get; set; }

        [Column("room_code")]
        public string? RoomCode { get; set; }

        [Column("room_department_id")]
        public int? RoomDepartmentId { get; set; }
        [Column("room_capacity")]
        public int? RoomCapacity { get; set; }
    }
}
