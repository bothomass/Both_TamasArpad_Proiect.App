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

        Figurines = new ObservableCollection<Figurine>();

        BindingContext = this;

        listView.ItemsSource = Figurines;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var figurines = await App.Database.GetFigurineAsync();
        Figurines.Clear();
        foreach (var figurine in figurines)
        {
            Figurines.Add(figurine);
        }
    }

    private void OnLikeClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        var figurine = (Figurine)button.BindingContext;

        figurine.LikeCounter++;

        Figurines[Figurines.IndexOf(figurine)] = figurine;

        App.Database.SaveFigurineAsync(figurine);
    }
    private void OnDislikeClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        var figurine = (Figurine)button.BindingContext;

        figurine.DislikeCounter++;

        Figurines[Figurines.IndexOf(figurine)] = figurine;

        App.Database.SaveFigurineAsync(figurine);
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        var figurine = (Figurine)button.BindingContext;

        Figurines.Remove(figurine);

        await App.Database.DeleteFigurineAsync(figurine);

        listView.ItemsSource = null;
        listView.ItemsSource = Figurines;
    }
}