using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;

namespace FileToolKit;
internal class JsonHandler
{
    private Dictionary<string, string> _extensionTypeMapping;
    public JsonHandler()
    {
        Read();
    }
    private void Read()
    {
        _extensionTypeMapping = new();
        // Obtener los recursos
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "FileToolKit.Assets.Json.FileIcons.json";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        // leemos el recurso
        using var reader = new StreamReader(stream);
        // Obtenemos el contenido
        var jsonContent = reader.ReadToEnd();
        // deseralizamos el contenido

        var jsonList = JsonConvert.DeserializeObject<List<Data>>(jsonContent);
        // Obtenemos una tupla con la extension y el nombre
        var extensionMap = jsonList
        .SelectMany(ft => ft.FileExtensions.Select(ext => (Extension: ext, ft.Name)));
        // creamos nuestro dictionary para poder facilitar la busqueda.
        foreach (var (Extension, Name) in extensionMap)
        {
            _extensionTypeMapping[Extension] = Name;
        }
    }
    public string GetFileImagenPath(string extension)
    {
        if (_extensionTypeMapping.ContainsKey(extension))
        {
            //var p = "C:\\Users\\sebas\\OneDrive\\Dev Home\\Projects\\C#\\FileOrganizerApp\\FileToolKit\\Assets\\Icons\\";
            return $"FileToolKit.Assets.Icons.{_extensionTypeMapping[extension]}.svg";
            //return Path.Combine(p, $"{_extensionTypeMapping[extension.ToLower()]}.svg");
            //return $"ms-appx:///Assets/Icons/{_extensionTypeMapping[Extension]}.svg";
        }
        return "FileToolKit.Assets.Icons.default.svg";
    }

    private class Data
    {
        public string Name { get; set; }
        public List<string> FileExtensions { get; set; }
    }
}
