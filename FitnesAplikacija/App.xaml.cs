using FitnesAplikacija.Services;
using FitnesAplikacija.Models;

namespace FitnesAplikacija
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }
        public static int ZadnjeKalorije { get; set; } = 0;
        public static int ZavrseniTreninzi { get; set; } = 0;
        public App()
        {
            InitializeComponent();

            Database = new DatabaseService();

            MainPage = new NavigationPage(new Start()); 
        }
        public static void ResetPodaciZaKorisnika()
        {
            ZadnjeKalorije = 0;
            ZavrseniTreninzi = 0;

        }

    }
}
