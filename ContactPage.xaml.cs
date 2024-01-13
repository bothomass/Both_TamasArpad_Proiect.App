using Both_TamasArpad_Proiect.Models;

namespace Both_TamasArpad_Proiect;

public partial class ContactPage : ContentPage
{
	public ContactPage()
	{
		InitializeComponent();
	}

    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        var contact = new Both_TamasArpad_Proiect.Models.Contact
        {
            Name = NameEntry.Text,
            Email = EmailEntry.Text,
            Phone = PhoneEntry.Text,
            Message = MessageEditor.Text
        };

        await App.Database.SaveContactAsync(contact);

        // Pe langa salvarea intr-o tabela, sa trimita si un e-mail cu aceste date

        await DisplayAlert("Success", "Message has been sent", "OK");

        NameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PhoneEntry.Text = string.Empty;
        MessageEditor.Text = string.Empty;

        await Navigation.PopAsync();
    }
}