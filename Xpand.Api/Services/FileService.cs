namespace Xpand.Api.Services;

public class FileService
{
    public async Task SaveFileAsync(IFormFile? file, string fileName)
    {
        if (file != null && file.Length > 0)
        {
            await using var stream = new FileStream($"./Uploads/{fileName}", FileMode.Create);
            await file.CopyToAsync(stream);
        }
        else
        {
            File.Copy($"./Uploads/default.png", $"./Uploads/{fileName}");
        }
    }

    public void DeleteFile(string fileName)
    {
        if (fileName != "default.png")
        {
            File.Delete($"./Uploads/{fileName}");
        }
    }
}
