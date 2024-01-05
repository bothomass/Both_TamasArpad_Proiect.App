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

    private void OnLikeClicked(object sender, EventArgs e)
    {
        // Get the Button from the sender
        var button = (Button)sender;

        // Get the Figurine associated with the Button
        var figurine = (Figurine)button.BindingContext;

        // Update Like count in the database
        figurine.LikeCounter++;

        // Update the ObservableCollection
        Figurines[Figurines.IndexOf(figurine)] = figurine;

        // Save to database or perform necessary operations
        App.Database.SaveFigurineAsync(figurine);
    }
    private void OnDislikeClicked(object sender, EventArgs e)
    {
        // Get the Button from the sender
        var button = (Button)sender;

        // Get the Figurine associated with the Button
        var figurine = (Figurine)button.BindingContext;

        // Update Dislike count in the database
        figurine.DislikeCounter++;

        // Update the ObservableCollection
        Figurines[Figurines.IndexOf(figurine)] = figurine;

        // Save to database or perform necessary operations
        App.Database.SaveFigurineAsync(figurine);
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        // Get the Button from the sender
        var button = (Button)sender;

        // Get the Figurine associated with the Button
        var figurine = (Figurine)button.BindingContext;

        // Remove the figurine from the ObservableCollection
        Figurines.Remove(figurine);

        // Remove the figurine from the database
        await App.Database.DeleteFigurineAsync(figurine);

        // Set the ItemsSource of the ListView to null and then reset it to Figurines
        // This ensures that the ListView updates properly
        listView.ItemsSource = null;
        listView.ItemsSource = Figurines;
    }
}