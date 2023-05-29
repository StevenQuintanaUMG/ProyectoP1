using ProyectoP1;

namespace ProyectoP1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();

        //Crear las tablas de la base de datos
        VehiculoDB vehiculoTB = new VehiculoDB();
        vehiculoTB.crearTablas();
    }

}
