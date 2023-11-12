namespace FileOrganizer.Core.Services;
public class FolderService
{
    public static IEnumerable<FileInfo> Read(string folderPath)
    {
        var folder = new DirectoryInfo(folderPath);
        // lectura de solo archivos que no esten ocultos
        var fileInfo = folder.EnumerateFiles().Where(file => (file.Attributes & FileAttributes.Hidden) == 0);
        return fileInfo;
    }

    public static void MoveFile(string sourceFilePath, string destinationFilePath)
    {
        File.Move(sourceFilePath, destinationFilePath);
    }
}
