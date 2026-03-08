using ScanToKnowDataAccess.Dto;

namespace ScanToKnowBusiness
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByIdServiceAsync(int id);
        Task<UserDto> CreateUserServiceAsync(UserDto user);
        Task<UserDto> UpdateUserServiceAsync(UpdateUserDto updateReq);

        Task<UserDto> LoginUserServiceAsync(UserDto user);
        Task<bool> DeleteAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentsServiceAsync();
        Task<List<PositionDto>> GetPositionsServiceAsync();
        Task<List<RoomDto>> GetRoomsServiceAsync();
        Task<List<ScheduleDto>> CreateScheduleServiceAsync(ScheduleDto schedule);
        Task<List<ScheduleDto>> GetSchedulesByUserIdServiceAsync(int userId);
        Task<List<AvailableRoomResponseDto>> GetAvailableRoomsServiceAsync(AvailableRoomDto availableRoomRequest);
        Task<ScheduleUpdateResponse> UpdateScheduleRoomServiceAsync(ScheduleUpdateRequest updateRequest);
        Task<RoomDto> GetRoomByIdServiceAsync(int id);
        Task<DepartmentDto> GetDepartmentByIdServiceAsync(int id);
        Task<UpdateStartOrEndResponse> UpdateScheduleStartOrEndServiceAsync(UpdateStartOrEndRequest updateRequest);

    }
}