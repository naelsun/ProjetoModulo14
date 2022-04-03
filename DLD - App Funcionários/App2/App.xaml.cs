using App2.Models;
using App2.Controllers;
using App2.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DBController dBController = new DBController();
            foreach (Funcionarios func in dBController.GetFuncionarios())
            {
                string androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                
                if (androidId == func.dispositivo && DateTimeOffset.Now.ToUnixTimeSeconds() < long.Parse(func.expira))
                {
                    MainPage = new NavigationPage(new LoggedPage());
                }
                else
                {
                    MainPage = new NavigationPage(new AccountPage());
                }
            }
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
