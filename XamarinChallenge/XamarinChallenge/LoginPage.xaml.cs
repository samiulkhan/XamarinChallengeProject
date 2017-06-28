using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAllianceApp.Models;

namespace XamarinChallenge
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool authenticated;
        public LoginPage()
        {
            InitializeComponent();
        }

        async void loginButton_Clicked(object sender, EventArgs e)
        {
            string mobileServiceClientUrl = "https://xamarinalliancesecurebackend.azurewebsites.net";
            MobileServiceClient client = new MobileServiceClient(mobileServiceClientUrl);
            //IMobileServiceTable<Character> CharacterTable = client.GetTable<Character>();

            //var gr = await CharacterTable.ToListAsync();
            // var dd = gr.GroupBy(x => x.Appearances.Select(y => y.Id));

            //var characters = await CharacterTable.ToListAsync();
            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.Authenticate(client);
            }
            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (authenticated == true)
                await Navigation.PushModalAsync(new MainPage(client));
        }
    }
}