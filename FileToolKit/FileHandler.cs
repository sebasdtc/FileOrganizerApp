using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FileToolKit;
public class FileHandler
{
    public static void Move(string sourceFileName, string destFileName)
    {
        File.Move(sourceFileName, destFileName);
    }
    public static async Task<List<FileTypeInfo>> Read(string path)
    {
        CoreResourcesMair core = new();
        var json = new JsonHandler();
        var fileTypeInfoList = new List<FileTypeInfo>();
        var directoryInfo = new DirectoryInfo(path);
        var fileInfo = directoryInfo.EnumerateFiles().Where(file => (file.Attributes & FileAttributes.Hidden) == 0);
        foreach (var file in fileInfo)
        {
            var pathImage = json.GetFileImagenPath(file.Extension[1..]);
            var svg = await core.LoadSvgFromStringAsync(pathImage);
            fileTypeInfoList.Add(new FileTypeInfo(file, svg));
        }
        return fileTypeInfoList;
    }
    public static SvgImageSource GetImageSource(string pathImage)
    {
        return new SvgImageSource(new Uri(pathImage));
    }
}
