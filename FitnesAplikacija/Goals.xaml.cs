using System;
using Microsoft.Maui.Controls;

namespace FitnesAplikacija;

public partial class Goals : ContentPage
{
    private int brojOznacenih = 0;

    public Goals()
    {
        InitializeComponent();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
            brojOznacenih++;
        else
            brojOznacenih--;

        System.Diagnostics.Debug.WriteLine($"Označeno: {brojOznacenih}");
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnPotvrdiButtonClicked(object sender, EventArgs e)
    {

        App.ZavrseniTreninzi = brojOznacenih;  
        await Navigation.PopAsync();             
    }
}
