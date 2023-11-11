using System.Windows.Controls;

namespace TheXDS.Ganymede.Views.CustomDialogs;

/// <summary>
/// Business logic for FilterEditorDialogView.xaml
/// </summary>
public partial class FilterEditorDialogView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FilterEditorDialogView"/>
    /// class.
    /// </summary>
    public FilterEditorDialogView()
    {
        InitializeComponent();
    }

    private void TabControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        ((TabControl)sender).SelectedIndex = 0;
    }
}
