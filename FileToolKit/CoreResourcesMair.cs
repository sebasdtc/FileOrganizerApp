using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.Storage;
using Windows.Storage.Streams;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FileToolKit;

public class CoreResourcesMair
{
    public string GetInformationalVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var customAttributes = assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);

        if (customAttributes.Length > 0)
        {
            var informationalVersionAttribute = (AssemblyInformationalVersionAttribute)customAttributes[0];
            return informationalVersionAttribute.InformationalVersion;
        }

        return string.Empty;
    }
    public string GetResources2ContentLeiaQuotes()
    {
        //
        //var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        //var directoryPath = Path.GetDirectoryName(assemblyLocation);
        // string filePath = Path.Combine(directoryPath, @"Resources2Content\LeiaQuotes.txt");


        //
        //string filePath = Path.Combine(Environment.CurrentDirectory, @"Resources2Content\LeiaQuotes.txt");


        return GetResources2ContentLeiaQuotes(directoryPath: AppDomain.CurrentDomain.BaseDirectory);

    }
    public string GetResources2ContentLeiaQuotes(string directoryPath)
    {

        if (!IsDirectoryPath(directoryPath))
        {
            throw new ArgumentException("Invalid directoryPath");
        }
        var filePath = Path.Combine(directoryPath, @"Resources2Content\LeiaQuotes.txt");

        using var reader = new StreamReader(filePath);
        if (reader != null)
        {
            return reader.ReadToEnd();
        }
        else
        {
            throw new Exception("Resource not found");
        }
    }
    public byte[] GetResources2ContentLeiaImage()
    {
        return GetResources2ContentLeiaImage(directoryPath: AppDomain.CurrentDomain.BaseDirectory);
    }
    public byte[] GetResources2ContentLeiaImage(string directoryPath)
    {

        if (!IsDirectoryPath(directoryPath))
        {
            throw new ArgumentException("Invalid directoryPath");
        }
        var filePath = Path.Combine(directoryPath, @"Resources2Content\Leia001.jpg");
        if (File.Exists(filePath))
        {
            return File.ReadAllBytes(filePath);
        }
        else
        {
            throw new Exception("Resource not found");
        }
    }
    public string GetResources3EmbeddedResourceDarthVaderQuotes(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(fileName);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        else
        {
            throw new Exception("Resource not found");
        }

    }
    public BitmapImage GetBitmapImagen(string fileName)
    {
        var imageBytes = GetResources3EmbeddedResourceDarthVaderImage(fileName);
        var bitmapImage = new BitmapImage();
        using var stream = new InMemoryRandomAccessStream();
        using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
        {
            writer.WriteBytes(imageBytes);
            writer.StoreAsync().GetResults();
        }
        bitmapImage.SetSource(stream);
        return bitmapImage;
    }
    public byte[] GetResources3EmbeddedResourceDarthVaderImage(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(fileName);
        if (stream != null)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        else
        {
            throw new Exception("Resource not found");
        }
    }
    public string GetSvgImageSource(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        //string fileName = $"Core.Resources3EmbeddedResource.{extension}.svg";

        using var stream = assembly.GetManifestResourceStream(fileName);
        if (stream != null)
        {
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        else
        {
            throw new Exception("Resource not found");
        }
    }
    public SvgImageSource LoadSvgFromString(string name)
    {
        var svgString = GetSvgImageSource(name);

        // Crear un archivo temporal para almacenar la cadena SVG
        var newFile = ApplicationData.Current.LocalFolder.CreateFileAsync(Guid.NewGuid().ToString() + ".svg").GetAwaiter().GetResult();

        // Escribir la cadena SVG en el archivo
        var writeStream = newFile.OpenStreamForWriteAsync().GetAwaiter().GetResult();
        writeStream.WriteAsync(Encoding.UTF8.GetBytes(svgString), 0, svgString.Length).GetAwaiter().GetResult();
        writeStream.Dispose();

        // Crear un SvgImageSource a partir del archivo
        var svgImageSource = new SvgImageSource(new Uri(newFile.Path));

        return svgImageSource;
    }
    public async Task<SvgImageSource> LoadSvgFromStringAsync(string name)
    {
        var svgString = GetSvgImageSource(name);
        // Crear un archivo temporal para almacenar la cadena SVG
        var newFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(Guid.NewGuid().ToString() + ".svg");

        // Escribir la cadena SVG en el archivo
        var writeStream = await newFile.OpenStreamForWriteAsync();
        await writeStream.WriteAsync(Encoding.UTF8.GetBytes(svgString), 0, svgString.Length);
        writeStream.Dispose();

        // Crear un SvgImageSource a partir del archivo
        var svgImageSource = new SvgImageSource(new Uri(newFile.Path));

        return svgImageSource;
    }

    public bool IsDirectoryPath(string path)
    {
        var directoryName = Path.GetDirectoryName(path);

        var isDirectoryPath = !string.IsNullOrEmpty(directoryName);

        return isDirectoryPath;
    }
}