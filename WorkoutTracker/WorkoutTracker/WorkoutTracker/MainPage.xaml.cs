using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutTracker
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            List<Exercise> workout = new List<Exercise>();
            workout.Add(new Exercise { Name = "bicep curl" });
            workout.Add(new Exercise { Name = "lat pull" });
            workout.Add(new Exercise { Name = "bench press" });
            workout.Add(new Exercise { Name = "shoulder press" });
            Exercises.ItemsSource = workout;
		}

    }
    
    public class Exercise
    {
        public string Name { get; set; }
    }
}
