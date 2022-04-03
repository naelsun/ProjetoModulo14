using App2.Controllers;
using App2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedPage : ContentPage
    {
        public LoggedPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            DBController dBController = new DBController();
            foreach (Funcionarios func in dBController.GetFuncionarios())
            {
                string androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

                if (androidId == func.dispositivo)
                {
                    txtName.Text = func.nome;
                    txtEmail.Text = func.email;
                }
            }
        }

        private async void editBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPage());
        }

        private async void addBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }

        private async void btnPassword_Clicked(object sender, EventArgs e)
        {
            string androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            DBController dBController = new DBController();

            PromptResult result = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                Message = "Insert Old Password",
                InputType = InputType.Password,
                Placeholder = "Password"
            });

            if (!result.Ok) return;

            foreach (Funcionarios func in dBController.GetFuncionarios())
            {
                if (func.dispositivo == androidId && result.Text == func.password)
                {
                    PromptResult resultNew = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                    {
                        Message = "Insert New Password",
                        InputType = InputType.Password,
                        Placeholder = "Password"
                    });

                    if (!result.Ok) return;

                    dBController.ChangePassword(func.nfunc, resultNew.Text);
                }
            }
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            string androidId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            DBController dBController = new DBController();

            foreach (Funcionarios func in dBController.GetFuncionarios())
            {
                if (func.dispositivo == androidId)
                {
                    dBController.Logout(func.nfunc);

                    await Navigation.PushAsync(new AccountPage());
                }
            }
        }
    }
}