namespace ProyectoP1;
public partial class UsarPickUp : ContentPage
{
    private string id;

    public UsarPickUp(string id)
    {
        InitializeComponent();
        this.id = id;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarDatosPickUp();
    }

    private void CargarDatosPickUp()
    {

        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickUp = vehiculoDB.ObtenerPickUpPorId(id);

        if (pickUp != null)
        {
            lblId.Text = pickUp.Id.ToString();
            lblMarca.Text = pickUp.Marca;
            lblModelo.Text = pickUp.Modelo;
            lblEs4x4.Text = pickUp.Es4x4 ? "Sí" : "No";
            lblCabina.Text = pickUp.Cabina;
            lblTorque.Text = pickUp.Torque.ToString();
            lblCarga.Text = pickUp.Carga.ToString();
            lblDiesel.Text = pickUp.Diesel ? "Sí" : "No";
        }
        else
        {

            DisplayAlert("Error", "No se encontró el PickUp con el ID especificado", "OK");
            Navigation.PopAsync();
        }

    }

    private void ButtonAcelerarClickP(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickUp = vehiculoDB.ObtenerPickUpPorId(id);
        pickUp.Acelerar(50);
    }


    private void ButtonFrenarClickP(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickup = vehiculoDB.ObtenerPickUpPorId(id);
        pickup.Frenar(50);
    }
    private void ButtonApagarClickP(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickup = vehiculoDB.ObtenerPickUpPorId(id);
        pickup.Apagar();
    }
    private void ButtonEncenderClickP(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickup = vehiculoDB.ObtenerPickUpPorId(id);
        pickup.Encender();
    }

    private void ButtonBocinaClickP(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        PickUp pickup = vehiculoDB.ObtenerPickUpPorId(id); ;
        pickup.Bocina();
    }
}

