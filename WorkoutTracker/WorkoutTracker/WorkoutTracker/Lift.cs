using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace WorkoutTracker
{
    [Table("Lifts")]
    public class Lift : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _exerciseName;
        [NotNull]
        public string ExerciseName
        {
            get
            {
                return _exerciseName;
            }
            set
            {
                this._exerciseName = value;
                OnPropertyChanged(nameof(ExerciseName));
            }
        }
        private string _muscle;
        [NotNull]
        public string Muscle
        {
            get
            {
                return _muscle;
            }
            set
            {
                this._muscle = value;
                OnPropertyChanged(nameof(Muscle));
            }
        }
        private int _weight;
        [NotNull]
        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                this._weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        private int _reps;
        [NotNull]
        public int Reps
        {
            get
            {
                return _reps;
            }
            set
            {
                this._reps = value;
                OnPropertyChanged(nameof(Reps));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
