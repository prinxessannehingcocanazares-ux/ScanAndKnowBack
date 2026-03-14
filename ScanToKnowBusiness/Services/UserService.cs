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

        public Task<List<DepartmentDto>> GetDepartmentsServiceAsync()
        {
            return _userRepository.GetDepartmentsRepoAsync();
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

            var createResponse = await _userRepository.CreateUserRepoAsync(userReq, department, position);

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
        public async Task<UserDto> UpdateUserServiceAsync(UpdateUserDto userReq)
        {

          
            var updateResponse = await _userRepository.UpdateUserRepoAsync(userReq);
            return updateResponse;
        }

        public Task<UserDto> LoginUserServiceAsync(UserDto user)
        {
            return _userRepository.LoginUserRepoAsync(user);
        }

   

        public Task<bool> DeleteAsync(int id)
        {

            return _userRepository.DeleteAsync(id);
        }

        public Task<bool> DeleteScheduleByIdServiceAync(int id)
        {

            return _userRepository.DeleteScheduleByIdRepoAync(id);
        }
        public Task<List<RoomDto>> GetRoomsServiceAsync()
        {
            return _userRepository.GetRoomsRepoAsync();
        }
        public async Task<List<ScheduleDto>> CreateScheduleServiceAsync(ScheduleDto schedule)
        {
            var results = new List<ScheduleDto>();

            if (schedule.ScheduleRepeatDaily == true && schedule.ScheduleDay != null)
            {
                DateTime scheduleDay = schedule.ScheduleDay.Value.ToDateTime(TimeOnly.MinValue);

                int diff = scheduleDay.DayOfWeek - DayOfWeek.Sunday;
                DateTime sunday = scheduleDay.AddDays(-diff);

                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDay = sunday.AddDays(i);

                    var newSchedule = new ScheduleDto
                    {
                        ScheduleSubject = schedule.ScheduleSubject,
                        ScheduleDay = DateOnly.FromDateTime(currentDay),
                        ScheduleStartTime = currentDay.Date + schedule.ScheduleStartTime.TimeOfDay,
                        ScheduleEndTime = currentDay.Date + schedule.ScheduleEndTime.TimeOfDay,
                        ScheduleRepeatWeekly = schedule.ScheduleRepeatWeekly,
                        ScheduleRepeatDaily = schedule.ScheduleRepeatDaily,
                        ScheduleRoomId = schedule.ScheduleRoomId,
                        ScheduleUserId = schedule.ScheduleUserId
                    };

                    var created = await _userRepository.CreateScheduleRepoAsync(newSchedule);
                    results.Add(created);
                }
            }
            else if (schedule.ScheduleRepeatWeekly == true && schedule.ScheduleDay != null)
            {
                DateOnly startDay = schedule.ScheduleDay.Value;

                DateOnly endDay = new DateOnly(startDay.Year, startDay.Month, DateTime.DaysInMonth(startDay.Year, startDay.Month));

                int totalDays = (endDay.ToDateTime(TimeOnly.MinValue) - startDay.ToDateTime(TimeOnly.MinValue)).Days;
                int weeks = (int)Math.Ceiling(totalDays / 7.0);

                for (int i = 0; i < weeks; i++)
                {
                    DateOnly currentDay = startDay.AddDays(i * 7);

                    int weekNumber = i + 1;

                    var newSchedule = new ScheduleDto
                    {
                        ScheduleSubject = schedule.ScheduleSubject,
                        ScheduleDay = currentDay,
                        ScheduleStartTime = currentDay.ToDateTime(TimeOnly.FromTimeSpan(schedule.ScheduleStartTime.TimeOfDay)),
                        ScheduleEndTime = currentDay.ToDateTime(TimeOnly.FromTimeSpan(schedule.ScheduleEndTime.TimeOfDay)),
                        ScheduleRepeatWeekly = schedule.ScheduleRepeatWeekly,
                        ScheduleRepeatDaily = schedule.ScheduleRepeatDaily,
                        ScheduleRoomId = schedule.ScheduleRoomId,
                        ScheduleUserId = schedule.ScheduleUserId
                    };

                    var created = await _userRepository.CreateScheduleRepoAsync(newSchedule);
                    results.Add(created);

                    Console.WriteLine($"Scheduled week {weekNumber}: {currentDay}");
                }
            }
            else
            {
                var created = await _userRepository.CreateScheduleRepoAsync(schedule);
                results.Add(created);
            }

            return results;
        }
        public Task<List<ScheduleDto>> GetSchedulesByUserIdServiceAsync(int userId)
        {
            return _userRepository.GetSchedulesByUserIdRepoAsync(userId);
        }
        public Task<List<ScheduleDto>> GetAllSchedulesServiceAsync()
        {
            return _userRepository.GetAllSchedulesRepoAsync();
        }

        public async Task<List<AvailableRoomResponseDto>> GetAvailableRoomsServiceAsync(AvailableRoomDto availableRoomRequest)
        {
            // 1. Get all schedules
            var schedules = await _userRepository.GetAllSchedulesRepoAsync();

            // 2. Filter schedules that conflict with requested time
            var conflictingSchedules = schedules.Where(s =>
                s.ScheduleRoomId.HasValue &&
                s.ScheduleStartTime < availableRoomRequest.End &&
                s.ScheduleEndTime > availableRoomRequest.Start
            ).ToList();

            // 3. Get all rooms and departments
            var allRooms = await _userRepository.GetRoomsRepoAsync();
            var allDepartments = await _userRepository.GetDepartmentsRepoAsync();

            // 4. Filter free rooms
            var freeRooms = allRooms
                .Where(r => !conflictingSchedules.Any(s => s.ScheduleRoomId == r.RoomId))
                .ToList();

            // 5. get departmentcollegename and set to AvailableRoomResponseDto.departmentCollegename where freeroom.roomdepartmentid == alldepartment.departmentid 
            var availableRooms = freeRooms.Select(r =>
            {
                var department = allDepartments.FirstOrDefault(d => d.DepartmentId == r.RoomDepartmentId);

                return new AvailableRoomResponseDto
                {
                    RoomId = r.RoomId,
                    RoomCode = r.RoomCode,
                    RoomDepartmentId = r.RoomDepartmentId,
                    DepartmentCollegeName = department?.DepartmentCollegeName ?? "Unknown",
                    RoomCapacity = r.RoomCapacity,
                };
            }).ToList();

            return availableRooms;
        }

        public async Task<ScheduleUpdateResponse> UpdateScheduleRoomServiceAsync(ScheduleUpdateRequest updateRequest)
        {
            return await _userRepository.UpdateScheduleRoomRepoAsync(updateRequest);
        }

        public async Task<RoomDto> GetRoomByIdServiceAsync(int id)
        {

            return await _userRepository.GetRoomByIdRepoAsync(id);
        }
        public async Task<DepartmentDto> GetDepartmentByIdServiceAsync(int id)
        {
            return await _userRepository.GetDepartmentByIdRepoAsync(id);
        }

        public async Task<UpdateStartOrEndResponse> UpdateScheduleStartOrEndServiceAsync(UpdateStartOrEndRequest updateRequest)
        {

            return await _userRepository.UpdateScheduleStartOrEndRepoAsync(updateRequest);
            
        }
    }
}