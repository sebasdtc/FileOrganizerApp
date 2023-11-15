using FileOrganizer.Core.Contracts.Services;
using FileToolKit;

namespace FileOrganizer.Core.Services;
public class FileTypeInfoService : IFileTypeInfoService
{
    private List<FileTypeInfo> _allFileTypeInfo;

    public async Task<IEnumerable<FileTypeInfo>> GetFileTypeInfoAsync(string folderPath)
    {
        _allFileTypeInfo ??= new List<FileTypeInfo>(FileHandler.Read(folderPath));
        return _allFileTypeInfo;
    }
}
