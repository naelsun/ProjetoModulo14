using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Controllers;
using App2.Models;
using Acr.UserDialogs;

namespace App2.Views
{
    public partial class ItemPage : ContentPage
    {
        public string num, destino;
        public ItemPage(string number, string status, string destination)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            num = number;
            destino = destination;
            lblItemNumber.Text = "Número: " + number;
            lblItemStatus.Text = "Estado: " + status;
            lblItemDestination.Text = "Destino: " + destination;
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage());
        }

        private async void accBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoggedPage());
        }

        private async void btnDelivered_Clicked(object sender, EventArgs e)
        {
            DBController dBController = new DBController();

            foreach (Encomendas encomenda in dBController.GetEncomendas())
            {
                if (encomenda.nencomenda == num)
                {
                    dBController.DeliverEncomenda(num);
                }

                await Navigation.PushAsync(new EditPage());
            }
        }

        private async void btnChangeDestination_Clicked(object sender, EventArgs e)
        {
            DBController dBController = new DBController();

            PromptResult result = await UserDialogs.Instance.PromptAsync(new PromptConfig()
            {
                Message = "Insert New Address",
                Text = destino
            });

            if (!result.Ok) return;

            foreach (Encomendas encomenda in dBController.GetEncomendas())
            {
                if (encomenda.nencomenda == num)
                {
                    dBController.ChangeEncomenda(num, result.Text);
                    lblItemDestination.Text = result.Text;
                }
            }
        }
    }
}
