using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using System.IO;
using Xamarin.Essentials;
using App1.models;


namespace App1
{
    public partial class MainPage : ContentPage
    {
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public string pathaudi, filename;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
           
            var permiso = await Permissions.RequestAsync<Permissions.Microphone>();
            var permiso1 = await Permissions.RequestAsync<Permissions.StorageRead>();
            var permiso2 = await Permissions.RequestAsync<Permissions.StorageWrite>();

            //hacemos comparacion de permisos
            if (permiso != PermissionStatus.Granted & permiso1 != PermissionStatus.Granted & permiso2 != PermissionStatus.Granted)
            {
                return;

            }//hacemos  una comparion del lado de texto
            if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                //mandamos una alerta de notificacion
                await DisplayAlert("Message", "Descripcion basia ", "Ok");
                return;
            }




            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                audioPlayer.Play(audioRecorderService.GetAudioFilePath());

            }
            else
            {
                await audioRecorderService.StartRecording();
            }

        }

        private async void btnpausa_Clicked(object sender, EventArgs e)
        {

            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                guardar();

                if (await DisplayAlert("Audio", "Reproducir", "SI", "NO"))
                {
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                }
                
            }
            else
            {
                await audioRecorderService.StartRecording();
            }



        }

        private async void btnlistar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new views.lista());
        }

        private async void guardar()
        {
            if (audioRecorderService.FilePath != null)
            {

                var storage = audioRecorderService.GetAudioFileStream();

                filename = Path.Combine(FileSystem.CacheDirectory, DateTime.Now.ToString("ddMMyyyymmss") + "_VoiceNote.wav");

                using (var fileStoage = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    storage.CopyTo(fileStoage);
                }

                pathaudi = filename;

            }
            audioclass audios = new audioclass();
            audios.url = pathaudi;
            audios.descripcion = txtdescripcion.Text;
            audios.fecha = DateTime.Now;


            var record = await App.dDBAudios.recordingaudio(audios);
            if (record ==1)
            {
                await DisplayAlert("", "Audio" + audios.descripcion + "Guardado en File" + audios.url, "Ok");
                txtdescripcion.Text = "";
            }
            else
            {
                await DisplayAlert("Warning", "No se pudo guardar", "Ok");
            }
        }
      
    }
}
