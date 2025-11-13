namespace FitnesAplikacija;

public partial class MaxKcal : ContentPage
{
    private const int DailyCaloriesGoal = 2000;
    private const double DailyWaterGoal = 2.0;

    public MaxKcal()
    {
        InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        int carbs = int.TryParse(CarbsEntry.Text, out var c) ? c : 0;
        int proteins = int.TryParse(ProteinsEntry.Text, out var p) ? p : 0;
        int fats = int.TryParse(FatsEntry.Text, out var f) ? f : 0;
        double water = double.TryParse(WaterEntry.Text, out var w) ? w : 0.0;

        int totalCalories = (carbs * 4) + (proteins * 4) + (fats * 9);
        App.ZadnjeKalorije = totalCalories;

        double caloriesProgress = Math.Min((double)totalCalories / DailyCaloriesGoal, 1.0);
        CaloriesProgressBar.Progress = caloriesProgress;
        CaloriesProgressLabel.Text = $"{totalCalories} / {DailyCaloriesGoal} kcal";

        double waterProgress = Math.Min(water / DailyWaterGoal, 1.0);
        WaterProgressBar.Progress = waterProgress;
        WaterProgressLabel.Text = $"{water:F1} / {DailyWaterGoal} L";

        SummaryLabel.Text = $"Unijeli ste {totalCalories} kcal i {water:F1} l vode.";

        if (totalCalories == 0)
            await DisplayAlert("Podsjetnik", "Unesite kalorije!", "OK");

        if (water == 0.0)
            await DisplayAlert("Podsjetnik", "Unesite unos vode!", "OK");


        DisplayAlert("Uspjeh", "Kalorije su spremljene!", "OK");
    }
}
