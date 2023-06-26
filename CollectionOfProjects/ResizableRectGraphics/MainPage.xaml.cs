using System.Drawing;
using ResizableRectGraphics.Drawables;
using ResizableRectGraphics.ViewModel;

namespace ResizableRectGraphics;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();  
    }

    private void OnStartInteraction(object sender, TouchEventArgs e)
    {
        var viewModel = BindingContext as MainVM;
        viewModel?.StartInteractionCommand.Execute(e);
    }

    private void OnDragInteraction(object sender, TouchEventArgs e)
    {
        var viewModel = BindingContext as MainVM;
        viewModel?.DragInteractionCommand.Execute(e);
    }

}
