using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
