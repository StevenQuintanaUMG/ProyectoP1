namespace ProyectoP1;

public partial class UsarSedan : ContentPage
{

    private string id;

    public UsarSedan(string id)
    {
        InitializeComponent();
        this.id = id;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarDatosSedan();
    }

    private void CargarDatosSedan()
    {

        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);

        if (sedan != null)
        {
            lblId.Text = sedan.Id.ToString();
            lblMarca.Text = sedan.Marca;
            lblModelo.Text = sedan.Modelo;
            lblPuertas.Text = sedan.Puertas.ToString();
            lblBaul.Text = sedan.CapacidadBaul.ToString();

        }
        else
        {

            DisplayAlert("Error", "No se encontró el Sedan con el ID especificado", "OK");
            Navigation.PopAsync();
        }

    }

    private void ButtonAcelerarClickSE(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);
        sedan.Acelerar(50);
    }

    private void ButtonFrenarClickSE(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);
        sedan.Frenar(50);
    }
    private void ButtonApagarClickSE(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);
        sedan.Apagar();
    }

    private void ButtonEncenderClickSE(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);
        sedan.Encender();
    }
    private void ButtonBocinaClickSE(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        Sedan sedan = vehiculoDB.ObtenerSedanPorId(id);
        sedan.Bocina();
    }

}