using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;

namespace FitnesAplikacija
{
    public partial class MainPage : ContentPage
    {
        private IDispatcherTimer _timer;
        private int _elapsedSeconds = 0;          
        private int _totalTrainingSeconds = 0;    
        private int brojZavrsenihTreninga;
        public MainPage()
        {
            InitializeComponent();
            TimerSection.IsVisible = false; 
            UpdateTimerLabel();
            UpdateTrainingTimeDisplay();
        }
        private readonly List<string> motivationalMessages = new List<string>
        {
             "Ostani dosljedan i zdrav!",
              "Jači si nego što misliš!",
             "Mali koraci vode do velikih promjena!",
              "Hidracija je ključ uspjeha!",
              "Svaki dan je nova prilika!"
        };


        public MainPage(int broj)
        {
            InitializeComponent();
            brojZavrsenihTreninga = broj;
            AzurirajZavrseneTreninge();

        }

        private void AzurirajZavrseneTreninge()
        {
            ZavrseniTreninziLabel.Text = brojZavrsenihTreninga.ToString();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AnimateMotivationalMessage();
            int kalorije = App.ZadnjeKalorije;
            TreninziLabel.Text = $"Unesene kalorije: {kalorije} kcal";

            brojZavrsenihTreninga = App.ZavrseniTreninzi;
            AzurirajZavrseneTreninge();

            AzurirajStatusZavrseniTreninziIKalorije();

            AzurirajStatusVrijeme();
        }
        private async void AnimateMotivationalMessage()
        {
            var random = new Random();
            string message = motivationalMessages[random.Next(motivationalMessages.Count)];
            MotivationalMessageLabel.Text = ""; 

            foreach (char c in message)
            {
                MotivationalMessageLabel.Text += c;
                await Task.Delay(40); 
            }
        }
        private void OnStartClicked(object sender, EventArgs e)
        {
            if (_timer == null)
            {
                _timer = Dispatcher.CreateTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (s, args) =>
                {
                    _elapsedSeconds++;
                    UpdateTimerLabel();
                };
            }

            _timer.Start();
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();

                _totalTrainingSeconds += _elapsedSeconds;
                _elapsedSeconds = 0;

                UpdateTimerLabel();
                UpdateTrainingTimeDisplay();

                AzurirajStatusVrijeme(); 
            }
        }
        private void AzurirajStatusZavrseniTreninziIKalorije()
        {
            int ciljZavrseniTreninzi = 2;
            int ciljKalorije = 1800;

            int brojZavrsenih = brojZavrsenihTreninga;
            int kalorije = App.ZadnjeKalorije;

            if (brojZavrsenih >= ciljZavrseniTreninzi)
                CiljStatusImageCiljevi.Source = "ispuniocilj.png";
            else if (brojZavrsenih > 0)
                CiljStatusImageCiljevi.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageCiljevi.Source = "ciljnijeispunjen.png";

            if (kalorije >= ciljKalorije)
                CiljStatusImageKalorije.Source = "ispuniocilj.png";
            else if (kalorije > ciljKalorije / 2)
                CiljStatusImageKalorije.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageKalorije.Source = "ciljnijeispunjen.png";
        }

        private void AzurirajStatusVrijeme()
        {
            int ciljVrijemeMin = 30;
            int vrijemeTreningaMin = _totalTrainingSeconds / 60;

            if (vrijemeTreningaMin >= ciljVrijemeMin)
                CiljStatusImageVrijeme.Source = "ispuniocilj.png";
            else if (vrijemeTreningaMin > ciljVrijemeMin / 2)
                CiljStatusImageVrijeme.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageVrijeme.Source = "ciljnijeispunjen.png";
        }

        private void UpdateTimerLabel()
        {
            TimeSpan time = TimeSpan.FromSeconds(_elapsedSeconds);
            TimerLabel.Text = time.ToString(@"mm\:ss");
        }

        private void UpdateTrainingTimeDisplay()
        {
            TimeSpan totalTime = TimeSpan.FromSeconds(_totalTrainingSeconds);
            TrainingTimeLabel.Text = totalTime.ToString(@"hh\:mm\:ss");
        }

        private void OnTimeTapped(object sender, EventArgs e)
        {
            TimerSection.IsVisible = !TimerSection.IsVisible;
        }

        private void AzurirajStatusCilja()
        {
            int ciljZavrseniTreninzi = 2;
            int ciljKalorije = 1800;
            int ciljVrijemeMin = 30;

            int brojZavrsenih = brojZavrsenihTreninga;
            int kalorije = App.ZadnjeKalorije;
            int vrijemeTreningaMin = _totalTrainingSeconds / 60;

            if (brojZavrsenih >= ciljZavrseniTreninzi)
                CiljStatusImageCiljevi.Source = "ispuniocilj.png";
            else if (brojZavrsenih > 0)
                CiljStatusImageCiljevi.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageCiljevi.Source = "ciljnijeispunjen.png";

            if (kalorije >= ciljKalorije)
                CiljStatusImageKalorije.Source = "ispuniocilj.png";
            else if (kalorije > ciljKalorije / 2)
                CiljStatusImageKalorije.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageKalorije.Source = "ciljnijeispunjen.png";

            if (vrijemeTreningaMin >= ciljVrijemeMin)
                CiljStatusImageVrijeme.Source = "ispuniocilj.png";
            else if (vrijemeTreningaMin > ciljVrijemeMin / 2)
                CiljStatusImageVrijeme.Source = "djelimicnoispunjen.png";
            else
                CiljStatusImageVrijeme.Source = "ciljnijeispunjen.png";
        }


        private async void OnBMITapped(object sender, EventArgs e) => await Navigation.PushAsync(new BMIcalc());
        private async void OnMaxTapped(object sender, EventArgs e) => await Navigation.PushAsync(new MaxKcal());
        private async void OnMinTapped(object sender, EventArgs e) => await Navigation.PushAsync(new MinKcal());
        private async void OnGoalTapped(object sender, EventArgs e) => await Navigation.PushAsync(new Goals());
        private async void OnSleepTapped(object sender, EventArgs e) => await Navigation.PushAsync(new PracenjeSna());
        private async void OnUserClicked(object sender, EventArgs e) => await Navigation.PushAsync(new UserProfile());
    }
}
