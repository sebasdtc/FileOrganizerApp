using System;
using System.IO;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FileToolKit;
public class FileTypeInfo
{
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Extension { get; set; }
    public DateTime CreationTime { get; set; }
    public SvgImageSource FileImage { get; set; }
    public long Length { get; set; }
    // Analizar mejoras
    public string Size 
        => Length < 102398976 ? $"{Length / 1024} KB" : $"{Length / (1024 * 1024)} KB";

    public FileTypeInfo(FileInfo fileInfo, SvgImageSource fileImage)
    {
        Name = fileInfo.Name;
        FullName = fileInfo.FullName;
        Extension = fileInfo.Extension;
        CreationTime = fileInfo.CreationTime;
        FileImage = fileImage;
        Length = fileInfo.Length;
    }
    public FileTypeInfo(FileInfo fileInfo)
    {
        Name = fileInfo.Name;
        FullName = fileInfo.FullName;
        Extension = fileInfo.Extension;
        CreationTime = fileInfo.CreationTime;
        FileImage = new SvgImageSource();
        Length = fileInfo.Length;
    }
}
