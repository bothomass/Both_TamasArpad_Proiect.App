// AddCreationPage.xaml.cs
using SQLite;
using Microsoft.Maui.Controls;
using Both_TamasArpad_Proiect.Models;
using System;
using System.IO;
using System.Collections.ObjectModel;

namespace Both_TamasArpad_Proiect
{
    public partial class AddCreationPage : ContentPage
    {
        private Creator selectedCreator;  // Add this line to declare the selectedCreator variable

        ObservableCollection<Creator> creators;

        public AddCreationPage()
        {
            InitializeComponent();
            creators = new ObservableCollection<Creator>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            creators.Clear();

            var allCreators = await App.Database.GetCreatorsAsync();
            foreach (var creator in allCreators)
            {
                // Check if the creator with the same Id already exists in the collection
                if (!creators.Any(c => c.Id == creator.Id))
                {
                    creators.Add(creator);
                }
            }

            CreatorPicker.ItemsSource = creators;
        }

        async void OnFigurineAdd(object sender, EventArgs e)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Figurine.db3");

                Figurine newFigurine = new Figurine
                {
                    Title = TitleEntry.Text,
                    Description = DescriptionEntry.Text,
                    CreatorId = selectedCreator?.Id ?? 0  // Assign the selected creator's Id
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

        // Add the following method to handle the selection of a creator
        async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedCreator = (Creator)picker.SelectedItem;
            }
        }

        void OnCreatorPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedCreator = (Creator)picker.SelectedItem;
            }
        }

        async void OnNavigateToAddCreatorPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCreatorPage());
        }
    }
}
