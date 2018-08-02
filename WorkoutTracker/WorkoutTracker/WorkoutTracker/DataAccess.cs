using System;
using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker
{
    class DataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Lift> Lifts { get; set; }

        public DataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Lift>();
            this.Lifts = new ObservableCollection<Lift>(database.Table<Lift>());
            //if table is empty, initialize collection
            if (!database.Table<Lift>().Any())
            {
                AddNewLift();
            }
        }

        public void AddNewLift()
        {
            this.Lifts.Add(new Lift { ExerciseName = "Row", Reps = 10, Weight = 120 , Muscle = "Back"});
        }

        public IEnumerable<Lift> GetFilteredLifts()
        {
            lock (collisionLock)
            {
                return database.Query<Lift>("SELECT * FROM Item WHERE Muscel = 'Back'").AsEnumerable();
            }
        }

    }
}
