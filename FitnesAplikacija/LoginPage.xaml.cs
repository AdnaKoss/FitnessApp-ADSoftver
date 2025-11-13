using FitnesAplikacija.Models;
using FitnesAplikacija.Services;
namespace FitnesAplikacija;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private async void OnButtonBackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Start());
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Greška", "Molimo ispunite oba polja!", "OK");
            return;
        }

        var user = await DatabaseService.GetUser(username, password);

        if (user != null)
        {
            UserSession.CurrentUser = user;
            await DisplayAlert("Dobrodošli", $"Zdravo, {user.Username}!", "OK");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Greška", "Neispravni podaci za prijavu.", "OK");
        }
    }

}