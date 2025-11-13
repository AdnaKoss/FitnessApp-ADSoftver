using FitnesAplikacija.Models;
using FitnesAplikacija.Services;


namespace FitnesAplikacija;

public partial class UserProfile : ContentPage
{
    public UserProfile()
    {
        InitializeComponent();
        LoadUserData();
    }

    private async void OnButtonBackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
    private async void OnLogOutButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Start());
        App.ResetPodaciZaKorisnika(); 
    }

    private void LoadUserData()
    {
        if (UserSession.CurrentUser != null)
        {
            UsernameLabel.Text = UserSession.CurrentUser.Username;
            BirthdateLabel.Text = UserSession.CurrentUser.DateOfBirth;
            EmailLabel.Text = UserSession.CurrentUser.Email;
            MobileLabel.Text = UserSession.CurrentUser.Mobile;
        }
        else
        {
            DisplayAlert("Greška", "Nema prijavljenog korisnika!", "OK");
        }
    }
}
