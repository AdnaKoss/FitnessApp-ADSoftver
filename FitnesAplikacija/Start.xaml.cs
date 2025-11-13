namespace FitnesAplikacija;

public partial class Start : ContentPage
{
    public Start()
    {
        InitializeComponent();
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}