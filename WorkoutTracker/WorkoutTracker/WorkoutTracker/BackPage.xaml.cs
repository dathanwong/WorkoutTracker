using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackPage : ContentPage
	{
        ObservableCollection<Exercise> workout = new ObservableCollection<Exercise>();

        public BackPage ()
		{
            InitializeComponent();
            workout.Add(new Exercise { Name = "bicep curl", Reps = 5, Weight = 100, Index = 0 });
            workout.Add(new Exercise { Name = "lat pull", Reps = 50, Weight = 100, Index = 1 });
            workout.Add(new Exercise { Name = "bench press", Reps = 213, Weight = 100, Index = 2 });
            workout.Add(new Exercise { Name = "shoulder press", Reps = 4, Weight = 100, Index = 3 });
            Exercises.ItemsSource = workout;
        }

        private void BtnMinusWeight_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Exercise t = (Exercise)b.CommandParameter;
            workout[t.Index].Weight = t.Weight - 5;
            refreshListView();
        }

        private void btnPlusWeight_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Exercise t = (Exercise)b.CommandParameter;
            workout[t.Index].Weight = t.Weight + 5;
            refreshListView();
        }

        private void btnMinusReps_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Exercise t = (Exercise)b.CommandParameter;
            workout[t.Index].Reps = t.Reps - 1;
            refreshListView();
        }

        private void btnPlusReps_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            Exercise t = (Exercise)b.CommandParameter;
            workout[t.Index].Reps = t.Reps + 1;
            refreshListView();
        }

        private void refreshListView()
        {
            Exercises.ItemsSource = null;
            Exercises.ItemsSource = workout;
        }

        public class Exercise
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public int Reps { get; set; }
            public int Weight { get; set; }
        }
    }


}