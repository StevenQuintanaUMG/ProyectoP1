using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP1.Clases
{
    internal class Vehiculo : IVehiculo
    {
 
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public int VelocidadMax { get; set; }

        public Vehiculo(string marca, string modelo, string color, int anio, string placa, string tipo, int velocidadMax)
        {
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Anio = anio;
            Placa = placa;
            Tipo = tipo;
            VelocidadMax = velocidadMax;
        }

        public bool VehiculoEncendido { get; set; }

        public void VelocidadActual(int velocidad)
        {
            var mensaje = "La velocidad actual es: " + velocidad + " km/h";
            MostrarAlerta(mensaje);
        }

        public void Bocina()
        {
            var mensaje = "¡PIP PIP!";
            MostrarAlerta(mensaje);
        }

        public void Acelerar(int cuanto)
        {
            if (!VehiculoEncendido)
            {
                MostrarAlerta("El vehículo está apagado. No se puede acelerar.");
                return;
            }

            var mensaje = "Acelerando " + cuanto + " km/h";
            MostrarAlerta(mensaje);
        }

        public void Encender()
        {
            if (VehiculoEncendido)
            {
                MostrarAlerta("El vehículo ya está encendido.");
                return;
            }

            VehiculoEncendido = true;
            MostrarAlerta("El vehículo está encendido.");
        }

        public void Apagar()
        {
            if (!VehiculoEncendido)
            {
                MostrarAlerta("El vehículo ya está apagado.");
                return;
            }

            VehiculoEncendido = false;
            MostrarAlerta("El vehículo está apagado.");
        }

        public void Frenar(int cuanto)
        {
            if (!VehiculoEncendido)
            {
                MostrarAlerta("El vehículo está apagado. No se puede frenar.");
                return;
            }

            var mensaje = "Frenando " + cuanto + " km/h";
            MostrarAlerta(mensaje);
        }

        private async void MostrarAlerta(string mensaje)
        {
            await Application.Current.MainPage.DisplayAlert("Mensaje", mensaje, "OK");
        }
    }


}
