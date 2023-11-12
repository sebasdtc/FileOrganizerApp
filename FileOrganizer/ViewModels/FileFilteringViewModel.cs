using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using FileOrganizer.Core.Contracts.Services;
using FileOrganizer.Core.Models;

namespace FileOrganizer.ViewModels;

public partial class FileFilteringViewModel : ObservableRecipient
{
    private readonly IFileTypeInfoService _fileTypeInfoService;
    public ObservableCollection<FileTypeInfo> Source { get; } = new ();
    public ObservableCollection<string> UniqueExtensions { get; set; } = new ();

    public FileFilteringViewModel(IFileTypeInfoService fileTypeInfoService)
    {
        _fileTypeInfoService = fileTypeInfoService;
        ShowFileTypeInfo();
        ShowFileTypeInfoGroupBy();
    }

    private async void ShowFileTypeInfo()
    {
        var path = "C:\\Users\\sebas\\Downloads\\";
        Source.Clear();
        var data = await _fileTypeInfoService.GetFileTypeInfoAsync(path);

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    private void ShowFileTypeInfoGroupBy()
    {
        var data = Source.Select(x => x.Extension).Distinct().ToList();
        foreach (var item in data)
        {
            UniqueExtensions.Add(item);
        }
    }
}
