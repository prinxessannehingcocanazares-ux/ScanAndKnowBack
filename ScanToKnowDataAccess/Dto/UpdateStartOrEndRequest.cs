public class UpdateStartOrEndRequest
{
    public int UserId { get; set; }
    public int ScheduleId { get; set; }
    public bool Start { get; set; }
    public bool End { get; set; }
}