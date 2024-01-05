using Both_TamasArpad_Proiect.Models;
using Microsoft.Maui.Controls;

namespace Both_TamasArpad_Proiect;

public partial class CreationPage : ContentPage
{
    public CreationPage()
    {
        InitializeComponent();
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