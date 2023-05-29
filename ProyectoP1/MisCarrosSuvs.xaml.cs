namespace ProyectoP1;

public partial class MisCarrosSuvs : ContentPage
{
    private string id;
    public MisCarrosSuvs()
	{
		InitializeComponent();


        VehiculoDB vehiculoDB = new VehiculoDB();
        List<SUV> Suvs = vehiculoDB.ObtenerSUVs();

        vehiculosCollectionView.ItemsSource = Suvs;
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