using ScanToKnowDataAccess.Dto;
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
                Id = u.UserId,
                FirstName = u.UserFirstName,
                LastName = u.UserLastName,
                Email = u.UserEmail,
                ContactNumber = u.UserContactNumber,
                Department = u.UserDepartment,
                Position = u.UserPosition,
                ProfilePicture = u.UserProfilePicture,
            }).ToList();
        }


        public async Task<UserDto?> GetUserByIdRepoAsync(int id)
        {
            var response = await _supabase
                .From<UserModel>()
                .Select("*")
                .Filter("user_id", Operator.Equals, id.ToString())
                .Single();


            if (response == null)
                return null;

            return new UserDto
            {
                Id = response.UserId,
                FirstName = response.UserFirstName,
                LastName = response.UserLastName,
                Email = response.UserEmail,
                ContactNumber = response.UserContactNumber,
                Department = response.UserDepartment,
                Position = response.UserPosition,
                ProfilePicture = response.UserProfilePicture,
            };
        }

        public async Task<UserDto> CreateUserRepoAsync(UserDto userDto,string department, string position)
        {
            var userModel = new UserModel
            {
                UserFirstName = userDto.FirstName,
                UserLastName = userDto.LastName,
                UserEmail = userDto.Email,
                UserContactNumber = userDto.ContactNumber,
                UserDepartment = department,
                UserPosition = position,
                UserProfilePicture = userDto.ProfilePicture,
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
                Id = inserted.UserId,
                FirstName = inserted.UserFirstName,
                LastName = inserted.UserLastName,
                Email = inserted.UserEmail,
                ContactNumber = inserted.UserContactNumber,
                Department = inserted.UserDepartment,
                Position = inserted.UserPosition,
                ProfilePicture = inserted.UserProfilePicture,
                Status = status

            };

        }

        public async Task<UserDto?> LoginUserRepoAsync(UserDto user)
        {

            var response = await _supabase
                .From<XUserModel>()
                .Select("*")
                .Filter("xUser_userName", Operator.Equals, user.UserName)
                .Filter("xUser_email", Operator.Equals, user.Email)
                .Filter("xUser_password", Operator.Equals, user.Password)
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
                Id = response.XUserUserId,
                UserName = response.XUserUserName,
                Email = response.XUserUserEmail,
                Role = response.XUserRole,
                Status = status
            };
        }

        public async Task<XUserDto> CreateXUserAsync(XUserDto xUserDto)
        {
            var xUserModel = new XUserModel
            {
                XUserUserId = xUserDto.XUserUserId,
                XUserUserName = xUserDto.XUserUserName,
                XUserUserEmail = xUserDto.XUserUserEmail,
                XUserUserPassword = xUserDto.XUserUserPassword,
                XUserDepartmentId = xUserDto.XUserDepartmentId,
                XUserPositionId = xUserDto.XUserPositionId,
                XUserUserOtp = xUserDto.XUserUserOtp,
                XUserOtpExpiry = xUserDto.XUserOtpExpiry,
                XUserRole = xUserDto.XUserRole,

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
                XUserId = inserted.XUserId,
                XUserUserName = inserted.XUserUserName,
                XUserUserEmail = inserted.XUserUserEmail,
                XUserUserPassword = inserted.XUserUserPassword,
                XUserDepartmentId = inserted.XUserDepartmentId,
                XUserPositionId = inserted.XUserPositionId,
                XUserUserOtp = inserted.XUserUserOtp,
                XUserOtpExpiry = inserted.XUserOtpExpiry,
                XUserRole = inserted.XUserRole,
                XUserStatus = status
            };

        }

        public async Task<List<DepartmentDto>> GetDepartmentsRepoAsync()
        {
            var response = await _supabase
                .From<DepartmentModel>()
                .Select("*")
                .Get();

            return response.Models.Select(u => new DepartmentDto
            {
                DepartmentId = u.DepartmentId,
                DepartmentCollegeName = u.DepartmentCollegeName,
            }).ToList();
        
        }
        public async Task<string> GetDepartmentByIdRepoAsync(string id)
        {
            var response = await _supabase
                .From<DepartmentModel>()
                .Select("*")
               .Filter("department_id", Operator.Equals, id.ToString())
               .Single();

            if (response == null)
                return null;

            return response.DepartmentCollegeName;

        }
        public async Task<List<PositionDto>> GetPositionsServiceAsync()
        {
            var response = await _supabase
                .From<PositionModel>()
                .Select("*")
                .Get();

            return response.Models.Select(u => new PositionDto
            {
                PositionId = u.PositionId,
                PositionTitle = u.PositionTitle,
            }).ToList();

        }
        public async Task<string> GetPositionByIdRepoAsync(string id)
        {
            var response = await _supabase
                .From<PositionModel>()
                .Select("*")
                .Filter("position_id", Operator.Equals, id.ToString())
               .Single();
            if (response == null)
                return null;

            return response.PositionTitle;
        }
        public async Task<UserDto?> UpdateAsync(UserDto userDto)
        {
            if (userDto.Id <= 0)
                return null; // invalid ID

            var updateModel = new UserModel
            {
                UserId = userDto.Id,
                UserFirstName = userDto.FirstName,
                UserLastName = userDto.LastName,
                UserEmail = userDto.Email,
                UserContactNumber = userDto.ContactNumber,
                UserDepartment = userDto.Department,
                UserPosition = userDto.Position,
                UserProfilePicture = userDto.ProfilePicture,

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
                Id = updated.UserId,
                FirstName = updated.UserFirstName,
                LastName = updated.UserLastName,
                Email = updated.UserEmail,
                ContactNumber = updated.UserContactNumber,
                Department = updated.UserDepartment,
                Position = updated.UserPosition,
                ProfilePicture = updated.UserProfilePicture,
                Status = status
            };
        }
        public async Task<bool> DeleteAsync(int id)
        {
            await _supabase
                .From<UserModel>()
                .Where(x => x.UserId == id)
                .Delete();

            return true;
        }

        public async Task<List<RoomDto>> GetRoomsRepoAsync()
        {
            var response = await _supabase
                .From<RoomModel>()
                .Select("*")
                .Get();

            return response.Models.Select(u => new RoomDto
            {
                RoomId = u.RoomId,
                RoomCode = u.RoomCode,
                RoomDepartmentId = u.RoomDepartmentId,
                RoomCapacity = u.RoomCapacity,
            }).ToList();

        }
        public async Task<ScheduleDto> CreateScheduleRepoAsync(ScheduleDto schedule)
        {
            var scheduleModel = new ScheduleModel
            {
                ScheduleSubject = schedule.ScheduleSubject,
                ScheduleDay = schedule.ScheduleDay,
                ScheduleStartTime = schedule.ScheduleStartTime,
                ScheduleEndTime = schedule.ScheduleEndTime,
                ScheduleRepeatWeekly = schedule.ScheduleRepeatWeekly,
                ScheduleUserId = schedule.ScheduleUserId,
            };

            var response = await _supabase
                .From<ScheduleModel>()
                .Insert(new List<ScheduleModel> { scheduleModel });

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

            return new ScheduleDto
            {
                ScheduleId = inserted.ScheduleId,
                ScheduleSubject = inserted.ScheduleSubject,
                ScheduleDay = inserted.ScheduleDay,
                ScheduleStartTime = inserted.ScheduleStartTime,
                ScheduleEndTime = inserted.ScheduleEndTime,
                ScheduleRepeatWeekly = inserted.ScheduleRepeatWeekly,
                ScheduleRoomId = inserted.ScheduleRoomId,
                ScheduleUserId = inserted.ScheduleUserId,
                ScheduleStatus = status

            };

        }

        public async Task<List<ScheduleDto>> GetSchedulesByUserIdRepoAsync(int userId)
        {
            var response = await _supabase
                .From<ScheduleModel>()
                .Select("*")
                .Filter("schedule_user_id", Operator.Equals, userId.ToString())
                .Get();
            return response.Models.Select(u => new ScheduleDto
            {
                ScheduleId = u.ScheduleId,
                ScheduleSubject = u.ScheduleSubject,
                ScheduleDay = u.ScheduleDay,
                ScheduleStartTime = u.ScheduleStartTime,
                ScheduleEndTime = u.ScheduleEndTime,
                ScheduleRepeatWeekly = u.ScheduleRepeatWeekly,
                ScheduleRoomId = u.ScheduleRoomId,
                ScheduleUserId = u.ScheduleUserId,
            }).ToList();
        }

    }
}