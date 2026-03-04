using Supabase.Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanToKnowDataAccess.Dto
{
    internal class ScheduleDto
    {
        public class ScheduleModel
        {
            public int? ScheduleId { get; set; }

            public string? ScheduleSubject { get; set; }

            public string? ScheduleDay { get; set; }

            public DateTime ScheduleStartTime { get; set; }

            public DateTime ScheduleEndTime { get; set; }

            public bool? ScheduleRepeatWeekly { get; set; }

            public int? ScheduleRoomId { get; set; }

            public int? ScheduleUserId { get; set; }
        }
    }
}
