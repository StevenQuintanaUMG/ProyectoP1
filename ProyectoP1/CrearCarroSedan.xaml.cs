using ProyectoP1.Clases;
using System;
using System.ComponentModel;
using System.Data.SQLite;

namespace ProyectoP1
{
    public partial class CrearCarroSedan : ContentPage, INotifyPropertyChanged
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

        public CrearCarroSedan()
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

        private void ColorButtonClickedSE(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            SelectedColor = selectedButton.BackgroundColor;

            string colorHex = SelectedColor.ToString();
            ColorBoxSE.Text = colorHex;

        }

        async void GuardarButtonClickedSE(object sender, EventArgs e)
        {
            // Obtener los valores de los Entry
            string marca = MarcaBoxSE.Text;
            string modelo = Convert.ToString(ModeloBoxSE.Text);
            string color = ColorBoxSE.Text;
            int anio = Convert.ToInt32(AnioBoxSE.Text);
            string placa = PlacaBoxSE.Text;
            string tipo = "Sedán";
            int velocidadMaxima = Convert.ToInt32(VelocidadMaxBoxSE.Text);
            int puertas = Convert.ToInt32(PuertasBoxSE.Text);
            int capacidadBaul = Convert.ToInt32(CapacidadBaulBoxSE.Text);

            // Crear una instancia del objeto Sedán con los valores ingresados
            Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMaxima);
            Sedan sedan = new Sedan(vehiculo, puertas, capacidadBaul);

            // Guardar el objeto Sedán en la base de datos utilizando VehiculoDB
            VehiculoDB vehiculoDB = new VehiculoDB();
            await Task.Run(() => vehiculoDB.InsertarVehiculo(sedan));

            await DisplayAlert("Éxito", "El vehículo ha sido guardado correctamente.", "Aceptar");

            await Navigation.PopAsync();
        }

        private void PuertasPickerSelectSE(object sender, EventArgs e){
                Picker picker = (Picker)sender;
                string selectedPuertas = (string)picker.SelectedItem;

                if (!string.IsNullOrEmpty(selectedPuertas))
                {
                    PuertasBoxSE.Text = selectedPuertas;
                }
            }
    }
 }

