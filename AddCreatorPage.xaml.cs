using SQLite;
using Microsoft.Maui.Controls;
using Both_TamasArpad_Proiect.Models;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Both_TamasArpad_Proiect
{
    public partial class AddCreatorPage : ContentPage, INotifyPropertyChanged
    {
        private Creator selectedCreator;
        private bool isAddEnabled = true;
        private bool isUpdateEnabled;
        private bool isDeleteEnabled;

        ObservableCollection<Creator> creators;

        public event PropertyChangedEventHandler PropertyChanged;

        public Creator SelectedCreator
        {
            get { return selectedCreator; }
            set
            {
                if (selectedCreator != value)
                {
                    selectedCreator = value;
                    OnPropertyChanged(nameof(SelectedCreator));
                }
            }
        }

        public AddCreatorPage()
        {
            InitializeComponent();
            creators = new ObservableCollection<Creator>();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

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

        public bool IsAddEnabled
        {
            get { return isAddEnabled; }
            set
            {
                if (isAddEnabled != value)
                {
                    isAddEnabled = value;
                    OnPropertyChanged(nameof(IsAddEnabled));
                }
            }
        }

        public bool IsUpdateEnabled
        {
            get { return isUpdateEnabled; }
            set
            {
                if (isUpdateEnabled != value)
                {
                    isUpdateEnabled = value;
                    OnPropertyChanged(nameof(IsUpdateEnabled));
                }
            }
        }

        public bool IsDeleteEnabled
        {
            get { return isDeleteEnabled; }
            set
            {
                if (isDeleteEnabled != value)
                {
                    isDeleteEnabled = value;
                    OnPropertyChanged(nameof(IsDeleteEnabled));
                }
            }
        }

        void OnCreatorPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedCreator = (Creator)picker.SelectedItem;


                IsUpdateEnabled = true;
                IsDeleteEnabled = true;
                IsAddEnabled = false; 
                                       
                FirstNameEntry.Text = selectedCreator.FirstName;
                LastNameEntry.Text = selectedCreator.LastName;
            }
            else
            {
                selectedCreator = null;


                IsUpdateEnabled = false;
                IsDeleteEnabled = false;
                IsAddEnabled = true;  
                               
                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;
            }
        }


        async void OnCreatorAdd(object sender, EventArgs e)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Figurine.db3");

                Creator newCreator = new Creator
                {
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text
                };

                var validationContext = new ValidationContext(newCreator, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(newCreator, validationContext, validationResults, validateAllProperties: true);

                if (!isValid)
                {
                    string errorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
                    await DisplayAlert("Validation Error", errorMessage, "OK");
                    return;
                }

                var database = new SQLiteAsyncConnection(dbPath);

                await database.InsertAsync(newCreator);

                await DisplayAlert("Success", "Creator added successfully", "OK");

                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        async void OnCreatorUpdate(object sender, EventArgs e)
        {
            if (selectedCreator != null)
            {
                selectedCreator.FirstName = FirstNameEntry.Text;
                selectedCreator.LastName = LastNameEntry.Text;

                var validationContext = new ValidationContext(selectedCreator, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(selectedCreator, validationContext, validationResults, validateAllProperties: true);

                if (!isValid)
                {
                    string errorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
                    await DisplayAlert("Validation Error", errorMessage, "OK");
                    return;
                }

                await App.Database.SaveCreatorAsync(selectedCreator);

                await DisplayAlert("Success", "Creator updated successfully", "OK");

                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;
            }
        }

        async void OnCreatorDelete(object sender, EventArgs e)
        {
            if (selectedCreator != null)
            {
                await App.Database.DeleteCreatorAsync(selectedCreator);

                await DisplayAlert("Success", "Creator deleted successfully", "OK");

                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;

            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            IsAddEnabled = !IsUpdateEnabled;
        }

    }
}
