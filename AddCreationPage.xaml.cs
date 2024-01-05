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
        protected override async void OnAppearing() 
        { 
            base.OnAppearing(); 
            listView.ItemsSource = await App.Database.GetFigurineAsync(); 
        } 
        async void OnFigurineAdd(object sender, EventArgs e) 
        { await Navigation.PushAsync(new CreationPage 
        { 
            BindingContext = new Figurine() 
        }); 
        } 
        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e) 
        { 
            if (e.SelectedItem != null) 
            { 
                await Navigation.PushAsync(new CreationPage
                { 
                    BindingContext = e.SelectedItem as Figurine
                }); 
            } 
        } 
    }
}