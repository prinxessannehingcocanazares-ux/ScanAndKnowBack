using Microsoft.AspNetCore.Mvc;
using ScanToKnowBusiness;
using ScanToKnowBusiness.Services;
using ScanToKnowDataAccess.Dto;
using ScanToKnowDataAccess.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISupabaseService _supabaseService;

    public UserController(IUserService userService, ISupabaseService supabaseService)
    {
        _userService = userService;
        _supabaseService = supabaseService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(UserDto user)
    {
        try
        {
            var response = await _userService.LoginUserServiceAsync(user);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromForm] UserSignUpRequest request)
    {
        try
        {
            string profileUrl = null;

            if (request.ProfilePicture != null && request.ProfilePicture.Length > 0)
            {
                profileUrl = await _supabaseService.UploadProfilePictureAsync(request.ProfilePicture);
            }

            // Map form data to UserDto
            var user = new UserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactNumber = request.ContactNumber,
                Department = request.Department,
                Position = request.Position,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
                ProfilePicture = profileUrl,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _userService.CreateUserServiceAsync(user);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("GetUserById")]
    public async Task<ActionResult<UserModel>> GetUserById(int id)
    {
        var response = await _userService.GetUserByIdServiceAsync(id);
        return Ok(response);
    }

    [HttpPost("GetDepartments")]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
        try
        {
            var departments = await _userService.GetDepartmentsServiceAsync();
            return Ok(departments);
        }catch(Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("GetPositions")]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
    {
        var positions = await _userService.GetPositionsServiceAsync();
        return Ok(positions);
    }

    [HttpPost("GetRooms")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
    {
        var positions = await _userService.GetRoomsServiceAsync();
        return Ok(positions);
    }

    [HttpPost("GetRoomById")]
    public async Task<ActionResult<RoomDto>> GetRoomById(int id)
    {
        try
        {
            var positions = await _userService.GetRoomByIdServiceAsync(id);
            return Ok(positions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("GetDepartmentById")]
    public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
    {
        try
        {
            var positions = await _userService.GetDepartmentByIdServiceAsync(id);
            return Ok(positions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpGet("GetUser")]
    public async Task<ActionResult<List<UserModel>>> GetUser()
    {
        var response = await _userService.GetAllAsync();
        return Ok(response);
    }

    [HttpPost("UpdateUser")]
    public async Task<ActionResult<UserDto>> UpdateUser(UserDto user)
    {
        var response = await _userService.UpdateAsync(user);
        return Ok(response);

    }

    [HttpPost("DeleteUser")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var response = await _userService.DeleteAsync(id);
        return Ok(response);
    }

    [HttpPost("CreateSchedule")]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule(ScheduleDto schedule)
    {
        try
        {
            var createSchedulResponse = await _userService.CreateScheduleServiceAsync(schedule);
            return Ok(createSchedulResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("GetSchedulesByUserId")]
    public async Task<ActionResult<List<ScheduleDto>>> GetSchedulesByUserId(int id)
    {
        try
        {
            var schedules = await _userService.GetSchedulesByUserIdServiceAsync(id);
            return Ok(schedules);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("GetAvailableRooms")]
    public async Task<ActionResult<List<AvailableRoomResponseDto>>> GetAvailableRooms(AvailableRoomDto availableRoomRequest)
    {
        try
        {
            var availableRooms = await _userService.GetAvailableRoomsServiceAsync(availableRoomRequest);
            return Ok(availableRooms);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("UpdateScheduleById")]
    public async Task<ActionResult<ScheduleUpdateResponse>> UpdateScheduleById(ScheduleUpdateRequest scheduleRequest)
    {
        try
        {
            var updateScheduleResponse = await _userService.UpdateScheduleRoomServiceAsync(scheduleRequest);
            return Ok(updateScheduleResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });
        }
    }

    [HttpPost("UpdateStartOrEnd")]
    public async Task<IActionResult> UpdateStartOrEnd([FromBody] UpdateStartOrEndRequest request)
    {
        try {
            if (!request.Start && !request.End)
                return BadRequest("Invalid action.");

            var updateResponse = await _userService.UpdateScheduleStartOrEndServiceAsync(request);


            return Ok(new
            {
                message = updateResponse.Message,
            });
        }catch(Exception ex)
        {
            return BadRequest(new { status = false, message = ex.Message });

        }
    }

}