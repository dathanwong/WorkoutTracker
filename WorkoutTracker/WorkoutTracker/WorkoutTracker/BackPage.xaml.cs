using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Diagnostics;

namespace WorkoutTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackPage : ContentPage
	{
        DataAccess dataAccess;
        String exerciseName = "back";

        public BackPage()
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
                List<Lift> backLifts = new List<Lift>();
                backLifts.Add(new Lift { ExerciseName = "Lat Pull Down", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Dumbbell Row", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Seated Row", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Deadlift ", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Bentover Barbell Row", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Reverse Fly", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Pull Up", Muscle = exerciseName, Reps = 0, Weight = 0 });
                backLifts.Add(new Lift { ExerciseName = "Angle Pull Down", Muscle = exerciseName, Reps = 0, Weight = 0 });
                foreach (Lift lift in backLifts)
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
            if (entryAddExercise.Text == null)
            {
                return;
            }
            else
            {
                Lift lift = new Lift { ExerciseName = entryAddExercise.Text, Muscle = exerciseName, Reps = 0, Weight = 0 };
                entryAddExercise.Text = "";
                await dataAccess.AddLiftAsync(lift);
                Exercises.ItemsSource = await dataAccess.GetFilteredLifts(exerciseName);
            }
        }

        //Delete Lift button
        private async void btnDeleteLift_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Lift lift = (Lift)Exercises.SelectedItem;
            if (lift != null)
            {
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


}