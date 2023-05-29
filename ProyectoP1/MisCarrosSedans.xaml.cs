namespace ProyectoP1;

public partial class MisCarrosSedans : ContentPage
{
    private string id;
    public MisCarrosSedans()
	{
		InitializeComponent();

        VehiculoDB vehiculoDB = new VehiculoDB();
        List<Sedan> Sedans = vehiculoDB.ObtenerSedans();

        vehiculosCollectionView.ItemsSource = Sedans;
    }

    private void EntryCompleted(object sender, EventArgs e)
    {
        if (sender is Entry entry)
        {
            id = entry.Text;
            Navigation.PushAsync(new UsarSedan(id));
        }
    }
}