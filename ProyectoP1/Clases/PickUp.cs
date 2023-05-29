using ProyectoP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP1
{
    internal class PickUp : Vehiculo
    {
        public bool Es4x4 { get; set; }
        public string Cabina { get; set; }
        public int Torque { get; set; }
        public int Carga { get; set; }
        public bool Diesel { get; set; }

        public PickUp(Vehiculo vehiculo, bool es4x4, string cabina, int torque, int carga, bool diesel)
            : base(vehiculo.Marca, vehiculo.Modelo, vehiculo.Color, vehiculo.Anio, vehiculo.Placa, vehiculo.Tipo, vehiculo.VelocidadMax)
        {
            Es4x4 = es4x4;
            Cabina = cabina;
            Torque = torque;
            Carga = carga;
            Diesel = diesel;
        }

    }


}