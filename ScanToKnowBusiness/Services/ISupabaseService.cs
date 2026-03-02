using Microsoft.AspNetCore.Http;

namespace ScanToKnowBusiness.Services
{
    public interface ISupabaseService
    {
        Task<string> UploadProfilePictureAsync(IFormFile file);
    }
}
