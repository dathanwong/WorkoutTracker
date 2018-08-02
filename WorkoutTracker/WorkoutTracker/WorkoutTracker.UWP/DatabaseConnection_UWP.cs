using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using WorkoutTracker.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace WorkoutTracker.UWP
{
    class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "WorkoutData.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }

    }
}
