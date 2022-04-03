using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Controllers;
using App2.Models;
using System.Collections.ObjectModel;

namespace App2.Views
{
    public partial class EditPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>();
        public string number, status, destination;
        public EditPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            DBController dBController = new DBController();

            foreach (Encomendas encomenda in dBController.GetEncomendas())
            {
                Items.Add(encomenda.nencomenda + "-" + encomenda.estado);
            }

            listView.ItemsSource = Items;
        }

        private async void accBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoggedPage());
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = e.Item;

            var nencomenda = tappedItem.ToString().Split('-')[0];

            DBController dBController = new DBController();

            foreach (Encomendas encomenda in dBController.GetEncomendas())
            {
                if (nencomenda == encomenda.nencomenda)
                {
                    if (encomenda.estado == "Entregue") return;
                    await Navigation.PushAsync(new ItemPage(encomenda.nencomenda, encomenda.estado, encomenda.destino));
                }
            }
        }

    }
}