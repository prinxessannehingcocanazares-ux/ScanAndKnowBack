namespace ScanToKnowDataAccess.Dto
{
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
        public int? RoomDepartmentId { get; set; }
        public int? RoomCapacity { get; set; }

    }
}
