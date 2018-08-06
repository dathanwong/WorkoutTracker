using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChestPage : ContentPage
    {
        DataAccess dataAccess;
        String exerciseName = "chest";

        public ChestPage()
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
                List<Lift> chestLifts = new List<Lift>();
                chestLifts.Add(new Lift { ExerciseName = "Dumbbell Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Pec Fly", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Incline Machine Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Cable Pec Fly", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Chest Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Incline Dumbbell Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                chestLifts.Add(new Lift { ExerciseName = "Cable Arm Raise", Muscle = exerciseName, Reps = 0, Weight = 0 });
                foreach (Lift lift in chestLifts)
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