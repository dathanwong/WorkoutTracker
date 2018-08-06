using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BicepPage : ContentPage
    {
        DataAccess dataAccess;
        String exerciseName = "bicep";

        public BicepPage()
        {
            InitializeComponent();
            //Establish SQLite Connection
            dataAccess = new DataAccess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Lifts.db3"));
        }

        protected override async void OnAppearing()
        {
            List<Lift> lifts = new List<Lift>();
            lifts = await dataAccess.GetFilteredLifts(exerciseName);
            //Initialize list of lifts if none exist
            if (lifts.Count == 0)
            {
                List<Lift> bicepLifts = new List<Lift>();
                bicepLifts.Add(new Lift { ExerciseName = "Sitting Dumbbell Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Hammer Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Barbell Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Cable Two Arm Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Preacher Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Reverse Dumbbell Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Reverse Barbell Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Sitting Isolation Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                bicepLifts.Add(new Lift { ExerciseName = "Cable One Arm Curl", Muscle = exerciseName, Reps = 0, Weight = 0 });
                foreach (Lift lift in bicepLifts)
                {
                    await dataAccess.AddLiftAsync(lift);
                }
            }
            Exercises.ItemsSource = lifts;
        }

        //Subtract weight button
        private async void BtnMinusWeight_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)b.CommandParameter;
            dataAccess.SubWeight(lift);
            Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
        }

        //Add weight button
        private async void btnPlusWeight_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)b.CommandParameter;
            dataAccess.AddWeight(lift);
            Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
        }

        //Subtract reps button
        private async void btnMinusReps_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)b.CommandParameter;
            dataAccess.SubRep(lift);
            Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
        }

        //Add reps button
        private async void btnPlusReps_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)b.CommandParameter;
            dataAccess.AddRep(lift);
            Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
        }
    }
}