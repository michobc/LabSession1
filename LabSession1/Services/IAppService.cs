using LabSession1.Models;

namespace LabSession1.Services;

public interface IAppService
{
    string GetDate(string culture);
    Task<string> UploadImage(Image image);
}