using Both_TamasArpad_Proiect.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Both_TamasArpad_Proiect;

public partial class CreationPage : ContentPage
{
    public ObservableCollection<Figurine> Figurines { get; set; }
    public CreationPage()
    {
        InitializeComponent();

        // Initialize the ObservableCollection
        Figurines = new ObservableCollection<Figurine>();

        // Set the binding context to this instance
        BindingContext = this;

        // Set the ItemsSource of the ListView to the Figurines collection
        listView.ItemsSource = Figurines;
    }

    private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            Figurines = new ObservableCollection<Figurine>();

            // Set the binding context to this instance
            BindingContext = this;

            // Set the ItemsSource of the ListView to the Figurines collection
            listView.ItemsSource = Figurines;
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load figurines from the database and update the ObservableCollection
        var figurines = await App.Database.GetFigurineAsync();
        Figurines.Clear(); // Clear existing items
        foreach (var figurine in figurines)
        {
            Figurines.Add(figurine);
        }
    }



    // Your data model
    private Figurine _creationData;

    public CreationPage(Figurine creationData)
    {
        InitializeComponent();

        _creationData = creationData;

        // Display creation details
        UpdateCreationDetails();
    }
    private void UpdateCreationDetails()
    {
        // Update labels with creation details
        // For example:
        // titleLabel.Text = _creationData.Title;
        // descriptionLabel.Text = _creationData.Description;
        // photoLocationLabel.Text = _creationData.PhotoLocation;
        // likesLabel.Text = $"Likes: {_creationData.LikeCounter}";
        // dislikesLabel.Text = $"Dislikes: {_creationData.DislikeCounter}";
    }

    private void OnLikeClicked(object sender, EventArgs e)
    {
        // Update Like count in the database
        _creationData.LikeCounter++;
        UpdateCreationDetails();
        // Save to database or perform necessary operations
    }

    private void OnDislikeClicked(object sender, EventArgs e)
    {
        // Update Dislike count in the database
        _creationData.DislikeCounter++;
        UpdateCreationDetails();
        // Save to database or perform necessary operations
    }
}