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
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Figurine.db3");

                Figurine newFigurine = new Figurine
                {
                    Title = TitleEntry.Text,
                    Description = DescriptionEntry.Text
                };

                var database = new SQLiteAsyncConnection(dbPath);

                await database.InsertAsync(newFigurine);

                await DisplayAlert("Success", "Figurine added successfully", "OK");

                TitleEntry.Text = string.Empty;
                DescriptionEntry.Text = string.Empty;

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}