namespace ScanToKnowDataAccess.Dto
{
    public class ScheduleDto
    {
        public int? ScheduleId { get; set; }

        public string? ScheduleSubject { get; set; }

        public string? ScheduleDay { get; set; }

        public TimeSpan ScheduleStartTime { get; set; }

        public TimeSpan ScheduleEndTime { get; set; }

        public bool? ScheduleRepeatWeekly { get; set; }

        public int? ScheduleRoomId { get; set; }

        public int? ScheduleUserId { get; set; }
        public bool? ScheduleStatus { get; set; }
    }
}