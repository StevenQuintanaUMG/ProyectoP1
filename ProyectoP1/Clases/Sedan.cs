using ProyectoP1.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP1
{
    internal class Sedan : Vehiculo
    {
        public int Puertas { get; set; }
        public int CapacidadBaul { get; set; }

        public Sedan(Vehiculo vehiculo, int puertas, int capacidadBaul)
            : base(vehiculo.Marca, vehiculo.Modelo, vehiculo.Color, vehiculo.Anio, vehiculo.Placa, vehiculo.Tipo, vehiculo.VelocidadMax)
        {
            Puertas = puertas;
            CapacidadBaul = capacidadBaul;
        }
    }

}
