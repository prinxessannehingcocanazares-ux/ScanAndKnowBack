using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ScanToKnowDataAccess.Models
{
    [Table("schedules")]
    public class ScheduleModel:BaseModel
    {
        [PrimaryKey("schedule_id")]
        public int? ScheduleId { get; set; }

        [Column("schedule_subject")]
        public string? ScheduleSubject { get; set; }
        
        [Column("schedule_day")]
        public DateOnly? ScheduleDay { get; set; }

        [Column("schedule_start_time")]
        public DateTime ScheduleStartTime { get; set; }
       
        [Column("schedule_end_time")]
        public DateTime ScheduleEndTime { get; set; }
       
        [Column("schedule_repeat_weekly")]
        public bool? ScheduleRepeatWeekly { get; set; }

        [Column("schedule_room_id")]
        public int? ScheduleRoomId { get; set; }

        [Column("schedule_user_id")]
        public int? ScheduleUserId { get; set; }

        [Column("schedule_start")]
        public DateTime? ScheduleStart { get; set; }
        [Column("schedule_end")]
        public DateTime? ScheduleEnd { get; set; }
    }
}
