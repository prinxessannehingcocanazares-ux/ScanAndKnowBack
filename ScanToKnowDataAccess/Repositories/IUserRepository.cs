using ScanToKnowDataAccess.Dto;

namespace ScanToKnowDataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> LoginUserRepoAsync(UserDto user);

        Task<XUserDto> CreateXUserAsync(XUserDto xUserDto);
        Task<UserDto?> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentsRepoAsync();
        Task<List<PositionDto>> GetPositionsRepoAsync();


    }
}