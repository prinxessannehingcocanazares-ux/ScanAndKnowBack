namespace ScanToKnowDataAccess.Dto
{
    public class ScheduleDto
    {
        public int? ScheduleId { get; set; }

        public string? ScheduleSubject { get; set; }

        public DateOnly? ScheduleDay { get; set; }

        public DateTime ScheduleStartTime { get; set; }

        public DateTime ScheduleEndTime { get; set; }

        public bool? ScheduleRepeatWeekly { get; set; }
        public bool? ScheduleRepeatDaily { get; set; }

        public int? ScheduleRoomId { get; set; }

        public int? ScheduleUserId { get; set; }
        public bool? ScheduleStatus { get; set; }
    }
}