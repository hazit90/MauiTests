using PaintOnCanvas.ViewModel;

namespace PaintOnCanvas;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void OnDragInteraction(object sender, TouchEventArgs e)
    {
        var viewModel = BindingContext as MainVM;
        viewModel?.DragInteractionCommand.Execute(e);
    }
}


