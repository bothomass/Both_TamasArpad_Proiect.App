using SQLite;
using Microsoft.Maui.Controls;
using Both_TamasArpad_Proiect.Models;

namespace Both_TamasArpad_Proiect

{
    public partial class AddCreationPage : ContentPage
    {
        // SQLite database connection
        private SQLiteAsyncConnection _database;

        public AddCreationPage()
        {
            InitializeComponent();

            // Initialize SQLite database (change the database path as needed)
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "Figurine.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<Figurine>().Wait();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            //// Save data to the SQLite database
            //var newData = new Figurines
            //{
            //    Title = TitleEntry.Text,
            //    Description = DescriptionEntry.Text,
            //    PhotoLocation = PhotoLocationEntry.Text,
            //    DateAdded = DateTime.Now
            //    //LikeCounter = Convert.ToInt32(LikeCounterEntry.Text),
            //    //DislikeCounter = Convert.ToInt32(DislikeCounterEntry.Text)
            //};
            //
            //await _database.InsertAsync(newData);
            //
            //// Optionally, navigate to another page or perform other actions after saving
            //// Navigation.PushAsync(new YourNextPage());
        }
    }
}