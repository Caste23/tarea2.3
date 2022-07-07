using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.AudioRecorder;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lista : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        models.audioclass recording;
        public lista()
        {
            InitializeComponent();
        }

        private void lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            recording = (models.audioclass)e.Item;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listas.ItemsSource = await App.dDBAudios.listaaudios();
        }

        private async void btnrepro_Invoked(object sender, EventArgs e)
        {
            if(recording != null)
            {
                var file = await App.dDBAudios.audiosobt(recording.id);
                audioPlayer.Play(file.url);

            }
            else
            {
                await DisplayAlert("Aviso", "click reproducir o eliminar", "Ok");
            }
        }

        private async void btneliminar_Invoked(object sender, EventArgs e)
        {
            if(await DisplayAlert("Warning","Desea eliminar audio?: "+recording.descripcion+"?", "si", "no"))
            {
                await App.dDBAudios.eliminarrecording(recording);
                await Navigation.PopAsync();
            }
        }

        
    }
}