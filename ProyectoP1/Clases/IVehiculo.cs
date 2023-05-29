using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP1
{
    internal interface IVehiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public int Anio { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public int VelocidadMax { get; }
        public void VelocidadActual(int velocidad);
        public void Bocina();
        public void Acelerar(int cuanto);
        public void Encender();
        public void Apagar();
        public void Frenar(int cuanto);
    }
}
