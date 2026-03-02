using ScanToKnowDataAccess.Models;
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


        public Task<UserDto> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            user.CreatedAt = DateTime.UtcNow;
            var response = await _userRepository.CreateUserAsync(user);

            bool xUserStatus = false;
            var xUserCreated = new XUserDto();
            if (response.Status)
            {
                var xUserData = new XUserDto
                {
                    UserId = response.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserPassword = user.Password,
                    UserOtp = user.Otp,
                    OtpExpiry = DateTime.UtcNow,
                    Role = "user"

                };
                xUserCreated = await _userRepository.CreateXUserAsync(xUserData);
                if (xUserCreated.Status)
                {
                    xUserStatus = true;
                }
            }

            if (xUserStatus)
            {
                response.UserName = xUserCreated.UserName;
                response.Password = xUserCreated.UserPassword;
                response.Otp = xUserCreated.UserOtp;
                response.Status = xUserStatus;
                return response;
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
    }
}