namespace FitnesAplikacija;

public partial class BMIcalc : ContentPage
{
    private string selectedGender = "Male";

    public BMIcalc()
    {
        InitializeComponent();
        UpdateGenderSelection();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnMaleTapped(object sender, TappedEventArgs e)
    {
        selectedGender = "Male";
        UpdateGenderSelection();
    }

    private void OnFemaleTapped(object sender, TappedEventArgs e)
    {
        selectedGender = "Female";
        UpdateGenderSelection();
    }

    private void UpdateGenderSelection()
    {
        Color selectedColor = Color.FromArgb("#66000000"); 
        Color unselectedColor = Color.FromArgb("#00FFFFFF"); 

        if (selectedGender == "Male")
        {
            MaleOption.BackgroundColor = selectedColor;
            FemaleOption.BackgroundColor = unselectedColor;
        }
        else
        {
            FemaleOption.BackgroundColor = selectedColor;
            MaleOption.BackgroundColor = unselectedColor;
        }
    }

    private void OnIzracunajBMIClicked(object sender, EventArgs e)
    {
        try
        {
            double heightInCm = double.Parse(HeightEntry.Text);
            double weightInKg = double.Parse(WeightEntry.Text);
            double heightInMeters = heightInCm / 100;
            double bmi = weightInKg / (heightInMeters * heightInMeters);

            string category = bmi switch
            {
                < 18.5 => "Pothranjenost",
                < 24.9 => "Normalna težina",
                < 29.9 => "Prekomjerna težina",
                _ => "Gojaznost"
            };

            ResultLabel.Text = $"Vaš BMI je: {bmi:F2}\nKategorija: {category}\nPol: {selectedGender}";
        }
        catch
        {
            ResultLabel.Text = "Molimo unesite ispravne brojeve za visinu i težinu.";
        }
    }
}
