using ScanToKnowDataAccess.Models;
using static Supabase.Postgrest.Constants;

namespace ScanToKnowDataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Supabase.Client _supabase;

        public UserRepository(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var response = await _supabase
                .From<UserModel>()
                .Select("*")
                .Get();

            return response.Models.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                ContactNumber = u.ContactNumber,
                Department = u.Department,
                Position = u.Position,
                ProfilePicture = u.ProfilePicture,
                CreatedAt = u.CreatedAt
            }).ToList();
        }


        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var response = await _supabase
                .From<UserModel>()
                .Select("*")
                .Filter("id", Operator.Equals, id.ToString())
                .Single();


            if (response == null)
                return null;

            return new UserDto
            {
                Id = response.Id,
                FirstName = response.LastName,
                Email = response.Email,
                ContactNumber = response.ContactNumber,
                Department = response.Department,
                Position = response.Position,
                ProfilePicture = response.ProfilePicture,
                CreatedAt = response.CreatedAt
            };
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var userModel = new UserModel
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                ContactNumber = userDto.ContactNumber,
                Department = userDto.Department,
                Position = userDto.Position,
                ProfilePicture = userDto.ProfilePicture,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _supabase
                .From<UserModel>()
                .Insert(new List<UserModel> { userModel });

            var inserted = response.Models.First();

            bool status = false;
            if (inserted == null)
            {
                return null;
            }
            else
            {
                status = true;
            }

            return new UserDto
            {
                Id = inserted.Id,
                FirstName = inserted.FirstName,
                LastName = inserted.LastName,
                Email = inserted.Email,
                ContactNumber = inserted.ContactNumber,
                Department = inserted.Department,
                Position = inserted.Position,
                ProfilePicture = inserted.ProfilePicture,
                CreatedAt = DateTime.UtcNow,
                Status = status

            };

        }

        public async Task<UserDto?> LoginUserRepoAsync(UserDto user)
        {

            var response = await _supabase
                .From<XUserModel>()
                .Select("*")
                .Filter("user_name", Operator.Equals, user.UserName)
                .Filter("user_email", Operator.Equals, user.Email)
                .Filter("user_password", Operator.Equals, user.Password)
                .Single();
            bool status = false;
            if (response == null)
            {
                return null;
            }
            else
            { status = true; }
            return new UserDto
            {
                Id = response.Id,
                UserName = response.UserName,
                Email = response.UserEmail,
                Role = response.Role,
                Status = status
            };
        }

        public async Task<XUserDto> CreateXUserAsync(XUserDto xUserDto)
        {
            var xUserModel = new XUserModel
            {
                UserName = xUserDto.UserName,
                UserEmail = xUserDto.UserEmail,
                UserPassword = xUserDto.UserPassword,
                UserOtp = xUserDto.UserOtp,
                OtpExpiry = xUserDto.OtpExpiry,

            };

            var response = await _supabase
                .From<XUserModel>()
                .Insert(new List<XUserModel> { xUserModel });

            var inserted = response.Models.First();

            bool status = false;
            if (inserted == null)
            {
                return null;
            }
            else
            {
                status = true;
            }

            return new XUserDto
            {
                Id = inserted.Id,
                UserName = inserted.UserName,
                UserEmail = inserted.UserEmail,
                UserPassword = inserted.UserPassword,
                UserOtp = inserted.UserOtp,
                OtpExpiry = inserted.OtpExpiry,
                Status = status
            };

        }
        public async Task<UserDto?> UpdateAsync(UserDto userDto)
        {
            if (userDto.Id <= 0)
                return null; // invalid ID

            var updateModel = new UserModel
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                ContactNumber = userDto.ContactNumber,
                Department = userDto.Department,
                Position = userDto.Position,
                ProfilePicture = userDto.ProfilePicture,
                CreatedAt = userDto.CreatedAt

            };

            // Use Filter BEFORE awaiting
            var response = await _supabase
                .From<UserModel>()
                .Filter("id", Operator.Equals, userDto.Id)  // <-- this is the filter
                .Update(updateModel);

            var updated = response.Models.FirstOrDefault();
            bool status = false;

            if (updated == null)
            {
                status = true;
                return null;
            }

            return new UserDto
            {
                Id = updated.Id,
                FirstName = updated.FirstName,
                LastName = updated.LastName,
                Email = updated.Email,
                ContactNumber = updated.ContactNumber,
                Department = updated.Department,
                Position = updated.Position,
                ProfilePicture = updated.ProfilePicture,
                CreatedAt = updated.CreatedAt,
                Status = status
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            await _supabase
                .From<UserModel>()
                .Where(x => x.Id == id)
                .Delete();

            return true;
        }


    }
}