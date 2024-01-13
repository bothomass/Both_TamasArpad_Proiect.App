using SQLite;
using Microsoft.Maui.Controls;
using Both_TamasArpad_Proiect.Models;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Both_TamasArpad_Proiect
{
    public partial class AddCreationPage : ContentPage
    {
        private Creator selectedCreator;

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
                    CreatorId = selectedCreator?.Id ?? 0 
                };

                var validationContext = new ValidationContext(newFigurine, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(newFigurine, validationContext, validationResults, validateAllProperties: true);

                if (!isValid)
                {
                    string errorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
                    await DisplayAlert("Validation Error", errorMessage, "OK");
                    return;
                }

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
