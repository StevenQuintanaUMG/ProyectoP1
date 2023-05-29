namespace ProyectoP1;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void GoToMisCarros(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MisCarros());
    }

    private void GoToCrearCarro(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CrearCarro());
    }
    private void ExitButton(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }


}

