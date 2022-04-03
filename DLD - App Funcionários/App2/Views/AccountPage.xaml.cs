using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Controllers;
using App2.Models;

namespace App2.Views
{
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string email = entEmail.Text;
            string pass = entPass.Text;

            DBController dbController = new DBController();

            foreach (Funcionarios func in dbController.GetFuncionarios())
            {
                if (email == func.email && pass == func.password)
                {
                    dbController.SetDevice(func.nfunc);

                    await Navigation.PushAsync(new LoggedPage());
                }
            }
        }
    }
}
