using Microsoft.AspNetCore.Http;
using ScanToKnowBusiness.Services;

public class SupabaseService : ISupabaseService
{
    private readonly Supabase.Client _supabase;
    private readonly string _bucketName = "profile";

    public SupabaseService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<string> UploadProfilePictureAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty", nameof(file));

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            throw new ArgumentException("Only JPG, JPEG, or PNG files are allowed");


        var fileName = $"profile/{Guid.NewGuid()}{extension}";
        var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

        var tempDir = Path.GetDirectoryName(tempFilePath);
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        using (var stream = new FileStream(tempFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        using (var fs = File.Create(tempFilePath))
            await file.CopyToAsync(fs);

        await _supabase.Storage
            .From(_bucketName)
            .Upload(tempFilePath, fileName, new Supabase.Storage.FileOptions
            {
                CacheControl = "3600",
                Upsert = true
            });

        var publicUrl = _supabase.Storage
        .From(_bucketName)
        .GetPublicUrl(fileName);

        return publicUrl;
    }
}