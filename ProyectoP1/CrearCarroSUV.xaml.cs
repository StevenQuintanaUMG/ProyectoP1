using ProyectoP1.Clases;
using System;
using System.ComponentModel;

namespace ProyectoP1
{
    public partial class CrearCarroSUV : ContentPage, INotifyPropertyChanged
    {
        private Color selectedColor;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        public CrearCarroSUV()
        {
            InitializeComponent();
            BindingContext = this;
            //GuardarButton.Clicked += GuardarButtonClicked;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ColorButtonClickedS(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            SelectedColor = selectedButton.BackgroundColor;

            string colorHex = SelectedColor.ToString();
            ColorBoxS.Text = colorHex;

        }

        async void GuardarButtonClickedS(object sender, EventArgs e)
        {
            // Obtener los valores de los Entry
            string marca = MarcaBoxS.Text;
            string modelo = Convert.ToString(ModeloBoxS.Text);
            string color = ColorBoxS.Text;
            int anio = Convert.ToInt32(AnioBoxS.Text);
            string placa = PlacaBoxS.Text;
            string tipo = "SUV";
            int velocidadMaxima = Convert.ToInt32(VelocidadMaxBoxS.Text);
            bool es4x4 = Es4x4SwitchS.IsToggled;
            int torque = Convert.ToInt32(TorqueBoxS.Text);

            // Crear una instancia del objeto SUV con los valores ingresados
            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMaxima);
            SUV suv = new SUV(vehiculo, es4x4, torque);

            // Guardar el objeto SUV en la base de datos utilizando VehiculoDB
            VehiculoDB vehiculoDB = new VehiculoDB();
            await Task.Run(() => vehiculoDB.InsertarVehiculo(suv));

            await DisplayAlert("Éxito", "El vehículo ha sido guardado correctamente.", "Aceptar");

            await Navigation.PopAsync();
        }

        private void Es4x4SwitchToggledS(object sender, ToggledEventArgs e)
        {
            bool is4x4 = e.Value;
            Es4x4LabelS.Text = is4x4 ? "SI" : "NO";
        }
    }
}
