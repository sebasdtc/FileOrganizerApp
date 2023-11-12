using FileOrganizer.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace FileOrganizer.Views;

public sealed partial class FileFilteringPage : Page
{
    public FileFilteringViewModel ViewModel
    {
        get;
    }

    public FileFilteringPage()
    {
        ViewModel = App.GetService<FileFilteringViewModel>();
        InitializeComponent();
    }
    private async void AppBarButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (EditDialog != null)
        {
            await EditDialog.ShowAsync();
            // Puedes añadir lógica adicional basada en el resultado si es necesario
        }
    }
}
