using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.models;
using System.IO;
using App1.controllers;
namespace App1
{
    public partial class App : Application
    {
        static dbaudios dbaudios;
        public static dbaudios dDBAudios
        {
            get
            {
                if (dbaudios==null)
                {
                    dbaudios = new dbaudios(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recording.db"));

                }
                return dbaudios;
            }
        }
    
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
