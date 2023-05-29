using ProyectoP1;
using ProyectoP1.Clases;
using System.Collections.Generic;


namespace ProyectoP1
{
    public partial class MisCarros : ContentPage
    {
        private VehiculoDB vehiculoDB;

        public MisCarros()
        {
            InitializeComponent();

        }

        private void GoToPickups(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisCarrosPickUp());
        }

        private void GoToSedans(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisCarrosSedans());
        }

        private void GoToSuvs(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisCarrosSuvs());
        }


    }
}



