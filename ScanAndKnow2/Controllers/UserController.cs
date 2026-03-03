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

            var response = await _userService.CreateUserAsync(user);

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
        var response = await _userService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost("GetDepartments")]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
    {
        var departments = await _userService.GetDepartmentsServiceAsync();
        return Ok(departments);
    }

    [HttpPost("GetPositions")]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
    {
        var positions = await _userService.GetPositionsServiceAsync();
        return Ok(positions);
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

}