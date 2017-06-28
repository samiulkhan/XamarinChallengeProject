using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAllianceApp.Models;
using XamarinChallenge.Models;


namespace XamarinChallenge
{
    public partial class MainPage : ContentPage
    {
        private List<MovieList> _listOfMovies;
        public List<MovieList> ListOfMovies { get { return _listOfMovies; } set { _listOfMovies = value; base.OnPropertyChanged(); } }

        public MainPage(MobileServiceClient client)
        {
            InitializeComponent();
            BindListview(client);
            GetStoreImage(client);
        }


        private async Task BindListview(MobileServiceClient client)
        {

            IMobileServiceTable<Character> CharacterTable = client.GetTable<Character>();
            var characters = await CharacterTable.ToListAsync();

            var movies = characters.SelectMany(x =>
                from item in x.Appearances
                select new
                {
                    Id = item.Id,
                    MovieName = item.Title
                }
            ).Distinct().ToList();

            List<MovieList> list = new List<MovieList>();

            foreach (var m in movies)
            {
                var c = characters.Where(x => x.Appearances.Select(p => p.Id).Contains(m.Id)).ToList();
                if (c != null)
                {
                    var mg = new MovieList();
                    foreach (var item in c)
                    {
                        mg.Characters.Add(item);
                        
                    }
                    mg.MovieName = m.MovieName;
                    list.Add(mg);

                }
            }
            ListOfMovies = list;
            uiListviewMovies.ItemsSource = ListOfMovies;
            uiListviewMovies.IsGroupingEnabled = true;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Character;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new DetailsPage(item));

            // Manually deselect item
            uiListviewMovies.SelectedItem = null;
        }

        
        private async void GetStoreImage(MobileServiceClient client)
        {
            var token = await client.InvokeApiAsync("/api/StorageToken/CreateToken");

            string storageAccountName = "xamarinalliance";

            StorageCredentials credentials = new StorageCredentials(token.ToString());

            CloudStorageAccount account = new CloudStorageAccount(credentials, storageAccountName, null, true);

            var cl = account.CreateCloudBlobClient();

            var container = cl.GetContainerReference("images");
            var blob = container.GetBlobReference("XAMARIN-Alliance-logo.png");

            MemoryStream stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);

            
            //var guid = await client.InvokeApiAsync("/api/XamarinAlliance/ReceiveCredit");
        }


    }
}
