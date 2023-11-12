using FileOrganizer.Core.Helpers;
using Newtonsoft.Json;

namespace FileOrganizer.Core.Services;
public class JsonService
{
    public static Dictionary<string, string> Read(string jsonPath)
    {
        try
        {
            var jsonContent = File.ReadAllText(jsonPath);
            var jsonList = JsonConvert.DeserializeObject<List<Data>>(jsonContent);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var extensionMap = jsonList
            .SelectMany(ft => ft.FileExtensions.Select(ext => (Extension: ext, Name: ft.Name)));

            foreach (var items in extensionMap)
            {
                dict[items.Extension] = items.Name;
            }

            return dict;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }
        return null;
    }
}
public class Data
{
    public string Name { get; set; } = string.Empty;
    public List<string> FileExtensions { get; set; } = new List<string>();
}