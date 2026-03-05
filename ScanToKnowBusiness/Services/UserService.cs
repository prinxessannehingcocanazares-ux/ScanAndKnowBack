using ScanToKnowDataAccess.Dto;
using ScanToKnowDataAccess.Repositories;

namespace ScanToKnowBusiness
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Supabase.Client _supabase;

        public UserService(IUserRepository userRepository, Supabase.Client supabase)
        {
            _userRepository = userRepository;
            _supabase = supabase;
        }

        public Task<List<UserDto>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }


        public Task<UserDto> GetUserByIdServiceAsync(int id)
        {
            return _userRepository.GetUserByIdRepoAsync(id);
        }

        public  Task<List<DepartmentDto>> GetDepartmentsServiceAsync()
        {
            return  _userRepository.GetDepartmentsRepoAsync();
        }
        public Task<List<PositionDto>> GetPositionsServiceAsync()
        {
            return _userRepository.GetPositionsServiceAsync();
        }
        public async Task<UserDto> CreateUserServiceAsync(UserDto userReq)
        {
            var department = await _userRepository.GetDepartmentByIdRepoAsync(userReq.Department);
            var position = await _userRepository.GetPositionByIdRepoAsync(userReq.Position);
            userReq.CreatedAt = DateTime.UtcNow;

            var createResponse = await _userRepository.CreateUserRepoAsync(userReq, department,position);

            bool xUserStatus = false;
            var xUserCreated = new XUserDto();
            if (createResponse.Status)
            {
                var xUserData = new XUserDto
                {
                    XUserUserId = createResponse.Id,
                    XUserUserName = userReq.UserName,
                    XUserUserEmail = createResponse.Email,
                    XUserUserPassword = userReq.Password,
                    XUserDepartmentId = createResponse.Department,
                    XUserPositionId = createResponse.Position,
                    XUserRole = "user"

                };
                xUserCreated = await _userRepository.CreateXUserAsync(xUserData);
                if (xUserCreated.XUserStatus)
                {
                    xUserStatus = true;
                }

            }

            if (xUserStatus)
            {
                createResponse.UserName = xUserCreated.XUserUserName;
                createResponse.Password = xUserCreated.XUserUserPassword;
                createResponse.Status = xUserStatus;
                return createResponse;
            }
            else
            {
                return null;
            }
        }

        public Task<UserDto> LoginUserServiceAsync(UserDto user)
        {
            return _userRepository.LoginUserRepoAsync(user);
        }

        public Task<UserDto?> UpdateAsync(UserDto user)
        {
            return _userRepository.UpdateAsync(user);
        }


        public Task<bool> DeleteAsync(int id)
        {

            return _userRepository.DeleteAsync(id);
        }

        public Task<List<RoomDto>> GetRoomsServiceAsync()
        {
            return _userRepository.GetRoomsRepoAsync();
        }
        public Task<ScheduleDto> CreateScheduleServiceAsync(ScheduleDto schedule)
        {
            return _userRepository.CreateScheduleRepoAsync(schedule);
        }
        public Task<List<ScheduleDto>> GetSchedulesByUserIdServiceAsync(int userId)
        {
            return _userRepository.GetSchedulesByUserIdRepoAsync(userId);
        }
    }
}