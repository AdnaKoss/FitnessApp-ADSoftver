using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace FitnesAplikacija
{
    public partial class MinKcal : ContentPage
    {
        public MinKcal()
        {
            InitializeComponent();
        }


        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(); 
        }

        private void OnWeightLossPlanSelected(object sender, EventArgs e)
        {
            SetPlanTitle("Plan za gubitak težine");
            GenerateWeeklyPlan("weight_loss");
        }

        private void OnMuscleGainPlanSelected(object sender, EventArgs e)
        {
            SetPlanTitle("Plan za mišićnu masu");
            GenerateWeeklyPlan("muscle_gain");
        }

        private void OnMaintainFitnessPlanSelected(object sender, EventArgs e)
        {
            SetPlanTitle("Plan za održavanje kondicije");
            GenerateWeeklyPlan("fitness_maintain");
        }

        private void SetPlanTitle(string title)
        {
            PlanTitle.Text = title;
            PlanTitle.IsVisible = true;
        }

        private void GenerateWeeklyPlan(string planType)
        {
            PlanContainer.Children.Clear();
            PlanContainer.IsVisible = true;

            var exercises = GetExercisesForPlan(planType);
            var days = new string[] { "Ponedjeljak", "Utorak", "Srijeda", "Četvrtak", "Petak", "Subota", "Nedjelja" };

            foreach (var day in days)
            {
                var dayFrame = new Frame
                {
                    BackgroundColor = Color.FromArgb("#353141"),
                    CornerRadius = 10,
                    Padding = 10,
                    Margin = new Thickness(0, 0, 0, 10)
                };

                var dayLayout = new VerticalStackLayout();

                dayLayout.Children.Add(new Label
                {
                    Text = day,
                    FontSize = 18,
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold
                });

                foreach (var exercise in exercises)
                {
                    var exerciseLayout = new HorizontalStackLayout
                    {
                        Spacing = 10
                    };

                    exerciseLayout.Children.Add(new CheckBox { Color = Color.FromArgb("#8F0854") });

                    exerciseLayout.Children.Add(new Label
                    {
                        Text = $"{exercise.Name} - {exercise.Sets} seta x {exercise.Duration} min",
                        FontSize = 16,
                        TextColor = Color.FromArgb("#d3d3d3"),
                        VerticalTextAlignment = TextAlignment.Center
                    });

                    dayLayout.Children.Add(exerciseLayout);
                }

                dayFrame.Content = dayLayout;
                PlanContainer.Children.Add(dayFrame);
            }
        }

        private List<Exercise> GetExercisesForPlan(string planType)
        {
            return planType switch
            {
                "weight_loss" => new List<Exercise>
                {
                    new Exercise { Name = "Plank", Sets = 3, Duration = 1 },
                    new Exercise { Name = "Skakanje užeta", Sets = 4, Duration = 2 },
                    new Exercise { Name = "Brzi hod", Sets = 5, Duration = 15 }
                },
                "muscle_gain" => new List<Exercise>
                {
                    new Exercise { Name = "Bench Press", Sets = 4, Duration = 10 },
                    new Exercise { Name = "Deadlift", Sets = 4, Duration = 8 },
                    new Exercise { Name = "Pull-Ups", Sets = 3, Duration = 6 }
                },
                "fitness_maintain" => new List<Exercise>
                {
                    new Exercise { Name = "Jogging", Sets = 3, Duration = 30 },
                    new Exercise { Name = "Bodyweight Squats", Sets = 4, Duration = 5 },
                    new Exercise { Name = "Yoga Stretching", Sets = 2, Duration = 20 }
                },
                _ => new List<Exercise>()
            };
        }
    }

    public class Exercise
    {
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Duration { get; set; }
    }
}
