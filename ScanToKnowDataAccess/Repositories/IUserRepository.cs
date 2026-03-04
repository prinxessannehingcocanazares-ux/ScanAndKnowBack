using ScanToKnowDataAccess.Dto;

namespace ScanToKnowDataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByIdRepoAsync(int id);
        Task<UserDto> CreateUserRepoAsync(UserDto userDto, string department, string position);
        Task<UserDto> LoginUserRepoAsync(UserDto user);

        Task<XUserDto> CreateXUserAsync(XUserDto xUserDto);
        Task<UserDto?> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentsRepoAsync();
        Task<string> GetDepartmentByIdRepoAsync(string id);
        Task<List<PositionDto>> GetPositionsServiceAsync();
        Task<string> GetPositionByIdRepoAsync(string id);
        Task<List<RoomDto>> GetRoomsRepoAsync();


    }
}