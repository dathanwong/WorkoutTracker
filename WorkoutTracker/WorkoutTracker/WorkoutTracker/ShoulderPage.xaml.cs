using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoulderPage : ContentPage
    {
        DataAccess dataAccess;
        String exerciseName = "shoulder";

        public ShoulderPage()
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
                List<Lift> shoulderLifts = new List<Lift>();
                shoulderLifts.Add(new Lift { ExerciseName = "Overhead Dumbbell Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                shoulderLifts.Add(new Lift { ExerciseName = "Dumbbell Side Raises", Muscle = exerciseName, Reps = 0, Weight = 0 });
                shoulderLifts.Add(new Lift { ExerciseName = "Cable Side Raises", Muscle = exerciseName, Reps = 0, Weight = 0 });
                shoulderLifts.Add(new Lift { ExerciseName = "Overhead Machine Press", Muscle = exerciseName, Reps = 0, Weight = 0 });
                shoulderLifts.Add(new Lift { ExerciseName = "Arm Circles", Muscle = exerciseName, Reps = 0, Weight = 0 });
                foreach (Lift lift in shoulderLifts)
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