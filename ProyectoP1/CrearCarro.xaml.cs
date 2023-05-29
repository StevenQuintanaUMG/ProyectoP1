using System;
using System.ComponentModel;

namespace ProyectoP1
{
    public partial class CrearCarro : ContentPage
    {

        public CrearCarro()
        {
            InitializeComponent();
        }

        private void GoToCrearCarroPickUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearCarroPickUp());
        }

        private void GoToCrearCarroSedan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearCarroSedan());
        }

        private void GoToCrearCarroSUV(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearCarroSUV());
        }

        


    }
}
