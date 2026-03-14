using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanToKnowDataAccess.Dto
{
    public class ScheduleUpdateRequest
    {
        public int? ScheduleId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? ScheduleEndTime { get; set; }
        public DateTime? ScheduleStartTime { get; set; }
        public string? ScheduleSubject { get; set; }
        public string? UpdateTag { get; set; }
    }
}
