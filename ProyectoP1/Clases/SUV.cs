using ProyectoP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP1
{
    internal class SUV : Vehiculo
    {
        public bool Es4x4 { get; set; }
        public int Torque { get; set; }

        public SUV(Vehiculo vehiculo, bool es4x4, int torque)
            : base(vehiculo.Marca, vehiculo.Modelo, vehiculo.Color, vehiculo.Anio, vehiculo.Placa, vehiculo.Tipo, vehiculo.VelocidadMax)
        {
            Es4x4 = es4x4;
            Torque = torque;
        }
    }

}