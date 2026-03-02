using Microsoft.AspNetCore.Http;

public class UserSignUpRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // File from FormData
    public IFormFile ProfilePicture { get; set; }
}