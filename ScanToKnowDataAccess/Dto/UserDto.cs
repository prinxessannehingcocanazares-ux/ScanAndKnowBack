public class UserDto
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public string? ContactNumber { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Role { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Otp { get; set; }
    public bool Status { get; set; }
}