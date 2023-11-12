using FileOrganizer.Core.Contracts.Services;
using FileOrganizer.Core.Models;

namespace FileOrganizer.Core.Services;
public class FileTypeInfoService : IFileTypeInfoService
{
    private List<FileTypeInfo> _allFileTypeInfo;
    private Dictionary<string, string> _extensionIconName;
    private IEnumerable<FileInfo> _fileInfoList;

    public FileTypeInfoService()
    { }

    public async Task<IEnumerable<FileTypeInfo>> GetFileTypeInfoAsync(string folderPath)
    {
        _allFileTypeInfo ??= new List<FileTypeInfo>(AllFileTypeInfo(folderPath));

        await Task.CompletedTask;
        return _allFileTypeInfo;
    }

    private IEnumerable<FileTypeInfo> AllFileTypeInfo(string folderPath)
    {
        // Cambiar a ruta relativa
        var path = "C:\\Users\\sebas\\OneDrive\\Dev Home\\Projects\\C#\\FileOrganizerApp\\FileOrganizer.Core\\Assets\\FileIcons.json";

        _extensionIconName = JsonService.Read(path);
        _fileInfoList = FolderService.Read(folderPath);
        
        var files = new List<FileTypeInfo>();

        foreach (var fileInfo in _fileInfoList)
        {
            files.Add(new FileTypeInfo()
            {
                Name = fileInfo.Name,
                Extension = fileInfo.Extension,
                Size = GetSize(fileInfo.Length),
                IconName = GetIconPath(fileInfo.Extension[1..]),
                CreationDate = fileInfo.CreationTime,
            });
        }

        return files;
    }

    private string GetIconPath(string extension)
    {
        if (_extensionIconName.ContainsKey(extension))
        {
            return $"ms-appx:///Assets/Img/{_extensionIconName[extension]}.svg";
        }
        return "ms-appx:///Assets/Img/3d.svg";
    }
    // convertimos de byte a kilobyte
    private static string GetSize(long size) => $"{((long)(size / 1024))} KB";
}
