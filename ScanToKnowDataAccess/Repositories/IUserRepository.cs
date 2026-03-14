using ScanToKnowDataAccess.Dto;

namespace ScanToKnowDataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByIdRepoAsync(int id);
        Task<UserDto> CreateUserRepoAsync(UserDto userDto, string department, string position);
        Task<UserDto> UpdateUserRepoAsync(UpdateUserDto userDto);

        Task<UserDto> LoginUserRepoAsync(UserDto user);

        Task<XUserDto> CreateXUserAsync(XUserDto xUserDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteScheduleByIdRepoAync(int id);

        Task<List<DepartmentDto>> GetDepartmentsRepoAsync();
        Task<string> GetDepartmentByIdRepoAsync(string id);
        Task<List<PositionDto>> GetPositionsServiceAsync();
        Task<string> GetPositionByIdRepoAsync(string id);
        Task<List<RoomDto>> GetRoomsRepoAsync();
        Task<ScheduleDto> CreateScheduleRepoAsync(ScheduleDto schedule);
        Task<List<ScheduleDto>> GetSchedulesByUserIdRepoAsync(int userId);

        Task<List<ScheduleDto>> GetAllSchedulesRepoAsync();
        Task<ScheduleUpdateResponse> UpdateScheduleRoomRepoAsync(ScheduleUpdateRequest updateRequest);
        Task<RoomDto> GetRoomByIdRepoAsync(int id);
        Task<DepartmentDto> GetDepartmentByIdRepoAsync(int id);
        Task<UpdateStartOrEndResponse> UpdateScheduleStartOrEndRepoAsync(UpdateStartOrEndRequest updateRequest);
        Task<UpdateStartOrEndResponse> UpdateScheduleEndOnlyAsync(UpdateStartOrEndRequest scheduleCheckerRequest);


    }
}