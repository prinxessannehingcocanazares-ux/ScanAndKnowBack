using ScanToKnowDataAccess.Dto;

namespace ScanToKnowBusiness
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> LoginUserServiceAsync(UserDto user);
        Task<UserDto?> UpdateAsync(UserDto user);
        Task<bool> DeleteAsync(int id);
        Task<List<DepartmentDto>> GetDepartmentsServiceAsync();
        Task<List<PositionDto>> GetPositionsServiceAsync();

    }
}