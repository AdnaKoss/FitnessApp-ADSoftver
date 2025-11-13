using FitnesAplikacija.Services;
using FitnesAplikacija.Models;

namespace FitnesAplikacija;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }
    private async void OnButtonBackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Start());
    }
    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
{
    string username = UsernameEntry.Text;
    string date = dateEntry.Text;
    string email = EmailEntry.Text;
    string mobile = MobileEntry.Text;
    string password = PasswordEntry.Text;
    string cpassword = CPasswordEntry.Text;

    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(email) ||
        string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(cpassword))
    {
        await DisplayAlert("Greška", "Molimo unesite sva polja za registraciju!", "OK");
        return;
    }

    if (!email.Contains("@") || (!email.EndsWith(".com") && !email.EndsWith(".ba") && !email.Contains("gmail")))
    {
        await DisplayAlert("Greška", "Unesite ispravan e-mail (npr. ime@gmail.com)!", "OK");
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(date, @"^\d{1,2}[./]\d{1,2}[./]\d{4}$"))
    {
        await DisplayAlert("Greška", "Unesite datum u ispravnom formatu (npr. 01.01.2000)!", "OK");
        return;
    }

    if (!System.Text.RegularExpressions.Regex.IsMatch(mobile, @"^\d+$"))
    {
        await DisplayAlert("Greška", "Broj telefona mora sadržavati samo cifre!", "OK");
        return;
    }

    if (password != cpassword)
    {
        await DisplayAlert("Greška", "Lozinke se ne podudaraju!", "OK");
        return;
    }

    var user = new User
    {
        Username = username,
        DateOfBirth = date,
        Email = email,
        Mobile = mobile,
        Password = password
    };

    var result = await DatabaseService.AddUser(user);
    if (result > 0)
    {
        await DisplayAlert("Uspjeh", "Registracija uspješna!", "OK");
        await Navigation.PushAsync(new LoginPage());
    }
    else
    {
        await DisplayAlert("Greška", "Došlo je do greške prilikom registracije.", "OK");
    }
}
}