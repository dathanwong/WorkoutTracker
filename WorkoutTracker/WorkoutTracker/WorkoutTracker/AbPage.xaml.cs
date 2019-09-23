using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbPage : ContentPage
    {
        DataAccess dataAccess;
        String exerciseName = "abs";

        public AbPage()
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
                List<Lift> abLifts = new List<Lift>();
                abLifts.Add(new Lift { ExerciseName = "Sit Up", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Plank", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Weighted Crunch", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "V Sits", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Russian Twist", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Static Leg Raise", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Leg Raises", Muscle = exerciseName, Reps = 0, Weight = 0 });
                abLifts.Add(new Lift { ExerciseName = "Knee ups", Muscle = exerciseName, Reps = 0, Weight = 0 });
                foreach (Lift lift in abLifts)
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

        //Add Lift button
        private async void btnAddLift_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = new Lift { ExerciseName = entryAddExercise.Text, Muscle = exerciseName, Reps = 0, Weight = 0 };
            entryAddExercise.Text = "";
            await dataAccess.AddLiftAsync(lift);
            Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
        }

        //Delete Lift button
        private async void btnDeleteLift_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)Exercises.SelectedItem;
            string action = await DisplayActionSheet("Exercise will be permanently deleted", "Cancel", "Delete");
            Debug.WriteLine("Action: " + action);
            if (action.Equals("Delete"))
            {
                await dataAccess.DeleteItemAsync(lift);
                Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
            }
        }
    }
}