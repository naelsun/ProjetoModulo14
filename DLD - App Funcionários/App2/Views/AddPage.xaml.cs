using App2.Controllers;
using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            entTrack.Text = RandomString(8) + "PT";
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage());
        }

        private async void accBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoggedPage());
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            DBController dBController = new DBController();

            foreach (Funcionarios func in dBController.GetFuncionarios())
            {
                string androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                if (androidId == func.dispositivo)
                {
                    dBController.AddEncomenda(func.nfunc, entTrack.Text, "Em distribuição", entAddress.Text);
                }
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}