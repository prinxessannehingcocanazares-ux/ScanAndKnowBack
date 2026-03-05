using ScanToKnowDataAccess.Dto;

namespace ScanToKnowBusiness
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByIdServiceAsync(int id);
        Task<UserDto> CreateUserServiceAsync(UserDto user);
        Task<UserDto> LoginUserServiceAsync(UserDto user);
        Task<UserDto?> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentsServiceAsync();
        Task<List<PositionDto>> GetPositionsServiceAsync();
        Task<List<RoomDto>> GetRoomsServiceAsync();
        Task<ScheduleDto> CreateScheduleServiceAsync(ScheduleDto schedule);
        Task<List<ScheduleDto>> GetSchedulesByUserIdServiceAsync(int userId);


    }
}