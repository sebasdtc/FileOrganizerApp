using FileToolKit;

namespace FileOrganizer.Core.Contracts.Services;
public interface IFileTypeInfoService
{
    public Task<IEnumerable<FileTypeInfo>> GetFileTypeInfoAsync(string folderPath);
}
