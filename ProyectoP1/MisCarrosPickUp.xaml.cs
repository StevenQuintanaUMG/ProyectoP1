namespace ProyectoP1;

public partial class MisCarrosPickUp : ContentPage
{
    private string id;
    public MisCarrosPickUp()
    {
        InitializeComponent();

        VehiculoDB vehiculoDB = new VehiculoDB();
        List<PickUp> pickUps = vehiculoDB.ObtenerPickUps();

        vehiculosCollectionView.ItemsSource = pickUps;
    }

    private void EntryCompleted(object sender, EventArgs e)
    {
        if (sender is Entry entry)
        {
            id = entry.Text;
            Navigation.PushAsync(new UsarPickUp(id));
        }
    }

}