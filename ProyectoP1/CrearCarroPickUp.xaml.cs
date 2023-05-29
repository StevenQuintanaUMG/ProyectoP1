using Microsoft.Maui.Controls;
using ProyectoP1.Clases;
using System;
using System.ComponentModel;
using System.Data.Entity;

namespace ProyectoP1
{
    public partial class CrearCarroPickUp : ContentPage, INotifyPropertyChanged
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

        public CrearCarroPickUp()
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

        private void ColorButtonClicked(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            SelectedColor = selectedButton.BackgroundColor;

            string colorHex = SelectedColor.ToString();
            ColorBox.Text = colorHex;
        }

        async void GuardarButtonClicked(object sender, EventArgs e)
        {
            try
            {

                // Obtener los valores de los Entry
                string marca = MarcaBox.Text;
                string modelo = Convert.ToString(ModeloBox.Text);
                string color = ColorBox.Text;
                int anio = Convert.ToInt32(AnioBox.Text);
                string placa = PlacaBox.Text;
                string tipo = "PickUp";
                int velocidadMaxima = Convert.ToInt32(VelocidadMaxBox.Text);

                bool es4x4 = Es4x4Switch.IsToggled;
                string cabina = CabinaBox.Text;
                int torque = Convert.ToInt32(TorqueBox.Text);
                int carga = Convert.ToInt32(CargaBox.Text);
                bool diesel = DieselSwitch.IsToggled;
           
                Vehiculo vehiculo = new Vehiculo(marca, modelo, color, anio, placa, tipo, velocidadMaxima);
                PickUp pickUp = new PickUp(vehiculo, es4x4, cabina, torque, carga, diesel);

                // Guardar el objeto PickUp en la base de datos utilizando VehiculoDB
                VehiculoDB vehiculoDB = new VehiculoDB();
                await Task.Run(() => vehiculoDB.InsertarVehiculo(pickUp));

           

                await DisplayAlert("Éxito", "El vehículo ha sido guardado correctamente.", "Aceptar");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ha ocurrido un error al guardar el vehículo: " + ex.Message, "Aceptar");
            }
        }




        private void CabinaPickerSelect(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            string selectedCabina = (string)picker.SelectedItem;

            if (!string.IsNullOrEmpty(selectedCabina))
            {
                CabinaBox.Text = selectedCabina;
            }
        }

        private void Es4x4SwitchToggled(object sender, ToggledEventArgs e)
        {
            bool is4x4 = e.Value;
            Es4x4Label.Text = is4x4 ? "SI" : "NO";
        }


        private void DieselSwitchToggled(object sender, ToggledEventArgs e)
        {
            bool isDiesel = e.Value;
            // Realiza las acciones necesarias según el valor del interruptor
            DieselLabel.Text = isDiesel ? "SI" : "NO";
        }

    }
}
