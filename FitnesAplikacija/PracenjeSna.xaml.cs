using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace FitnesAplikacija
{
    public partial class PracenjeSna : ContentPage
    {
        public ObservableCollection<SleepRecord> SleepHistory { get; set; }

        public PracenjeSna()
        {
            InitializeComponent();

            SleepHistory = new ObservableCollection<SleepRecord>();
            SleepHistoryView.ItemsSource = SleepHistory;

            // Postavi vrijeme spavanja i buđenja
            SleepTimePicker.Time = new TimeSpan(22, 30, 0);
            WakeTimePicker.Time = new TimeSpan(6, 30, 0);

            LoadHistoryFromPreferences(); 
        }

        private void OnSaveSleepData(object sender, EventArgs e)
        {
            TimeSpan sleepTime = SleepTimePicker.Time;
            TimeSpan wakeTime = WakeTimePicker.Time;

            if (SleepQualityPicker.SelectedIndex == -1)
            {
                ResultLabel.Text = "Molimo ocijenite kvalitet sna.";
                return;
            }

            double hoursSlept = (wakeTime - sleepTime).TotalHours;
            if (hoursSlept < 0)
                hoursSlept += 24;

            var record = new SleepRecord
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy"),
                Duration = $"Trajanje sna: {Math.Round(hoursSlept, 1)}h",
                Quality = $"Kvalitet: {SleepQualityPicker.SelectedItem}"
            };

            SleepHistory.Insert(0, record);
            SaveHistoryToPreferences(); 

            ResultLabel.Text = "✅ San je uspješno sačuvan!";
        }

        private void SaveHistoryToPreferences()
        {
            try
            {
                string json = JsonSerializer.Serialize(SleepHistory);
                Preferences.Set("SleepHistory", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri čuvanju: {ex.Message}");
            }
        }

        private void LoadHistoryFromPreferences()
        {
            try
            {
                string json = Preferences.Get("SleepHistory", null);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var records = JsonSerializer.Deserialize<ObservableCollection<SleepRecord>>(json);
                    if (records != null)
                    {
                        foreach (var record in records)
                            SleepHistory.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri učitavanju: {ex.Message}");
            }
        }

        public class SleepRecord
        {
            public string Date { get; set; }
            public string Duration { get; set; }
            public string Quality { get; set; }
        }
    }
}
