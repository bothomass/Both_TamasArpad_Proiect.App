using SQLite;
using Microsoft.Maui.Controls;
using Both_TamasArpad_Proiect.Models;

namespace Both_TamasArpad_Proiect

{
    public partial class AddCreationPage : ContentPage
    {
        public AddCreationPage()
        {
            InitializeComponent();
        }

        async void OnFigurineAdd(object sender, EventArgs e)
        {
            try
            {
                // Print the database path to the console
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Figurine.db3");
                Console.WriteLine($"Database Path: {dbPath}");

                Figurine newFigurine = new Figurine
                {
                    Title = TitleEntry.Text,
                    Description = DescriptionEntry.Text
                };

                // Initialize SQLite database
                var database = new SQLiteAsyncConnection(dbPath);

                // Save the newFigurine to the database
                await database.InsertAsync(newFigurine);

                // Display an alert with a message
                await DisplayAlert("Success", "Figurine added successfully", "OK");

                // Navigate back to the previous page
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Log or display the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}