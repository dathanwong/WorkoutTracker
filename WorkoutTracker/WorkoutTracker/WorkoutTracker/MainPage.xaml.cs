using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutTracker
{
	public partial class MainPage : ContentPage
	{

        List<Exercise> workout = new List<Exercise>();
        ExerciseList exercises;

        public MainPage()
		{
			InitializeComponent();
            workout.Add(new Exercise { Name = "bicep curl", Reps = 5, Weight = 100, Index = 0});
            workout.Add(new Exercise { Name = "lat pull", Reps = 50, Weight = 100, Index = 1 });
            workout.Add(new Exercise { Name = "bench press", Reps = 213, Weight = 100, Index = 2 });
            workout.Add(new Exercise { Name = "shoulder press", Reps = 4, Weight = 100, Index = 3 });
            exercises = new ExerciseList(workout);
            Exercises.ItemsSource = exercises.Exercises;
		}

        private void BtnMinusWeight_Clicked(object sender, EventArgs e)
        {
            int selectedItem = (int) (sender as Button).CommandParameter;
            Exercise exercise = exercises.Exercises[selectedItem];
            exercises.Exercises[selectedItem].Weight = exercise.Weight - 5;
        }
    }

    public class Exercise
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
    }

    public class ExerciseList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Exercise> _exercises;

        public ObservableCollection<Exercise> Exercises
        {
            get { return _exercises; }
            set { _exercises = value; OnPropertyChanged("Exercises"); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExerciseList(List<Exercise> exerciseList)
        {
            Exercises = new ObservableCollection<Exercise>();
            foreach (Exercise ex in exerciseList)
            {
                Exercises.Add(ex);
            }
        }
    }
}
