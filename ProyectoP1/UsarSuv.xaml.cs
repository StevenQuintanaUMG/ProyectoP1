namespace ProyectoP1;

public partial class UsarSuv : ContentPage
{
    private string id;
    public UsarSuv(string id)
	{
		InitializeComponent();
        this.id = id;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarDatosSUV();
    }

    private void CargarDatosSUV()
    {

        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);

        if (suv != null)
        {
            lblId.Text = suv.Id.ToString();
            lblMarca.Text = suv.Marca;
            lblModelo.Text = suv.Modelo;
            lblTorque.Text = suv.Torque.ToString();
            lblEs4x4.Text = suv.Es4x4.ToString();

        }
        else
        {

            DisplayAlert("Error", "No se encontró el SUV con el ID especificado", "OK");
            Navigation.PopAsync();
        }

    }

    private void ButtonAcelerarClickS(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);
        suv.Acelerar(50);
    }

    private void ButtonFrenarClickS(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);
        suv.Frenar(50);
    }
    private void ButtonApagarClickS(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);
        suv.Apagar();
    }

    private void ButtonEncenderClickS(object sender, EventArgs e)
    {
        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);
        suv.Encender();
    }

    private void ButtonBocinaClickS(object sender, EventArgs e)
    {

        VehiculoDB vehiculoDB = new VehiculoDB();
        SUV suv = vehiculoDB.ObtenerSUVPorId(id);
        suv.Bocina();
    }
}