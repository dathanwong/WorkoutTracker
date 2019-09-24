using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutTracker
{
    class DataAccess
    {
        readonly SQLiteAsyncConnection database;

        public DataAccess(string dbPath)
        {
            //Create new SQLite table
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Lift>().Wait();
        }

        //Get all lifts
        public Task<List<Lift>> GetLifts()
        {
            return database.Table<Lift>().ToListAsync();
        }

        //Add new lift
        public Task<int> AddLiftAsync(Lift item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }

        }

        //Delete lift
        public Task<int> DeleteItemAsync(Lift item)
        {
            return database.DeleteAsync(item);
        }

        public async void DeleteAllItems()
        {
            List<Lift> all = await GetLifts();
            foreach (Lift lift in all)
            {
                await DeleteItemAsync(lift);
            }

        }

        //Add weight
        public int AddWeight(Lift lift)
        {
            int newWeight = lift.Weight + 5;
            database.QueryAsync<Lift>("UPDATE LIFTS SET WEIGHT =" + newWeight.ToString() + " WHERE ID = " + lift.Id);
            return newWeight;
        }

        //Add weight long press
        public int AddWeightLongPress(Lift lift)
        {
            int newWeight = lift.Weight + 10;
            database.QueryAsync<Lift>("UPDATE LIFTS SET WEIGHT =" + newWeight.ToString() + " WHERE ID = " + lift.Id);
            return newWeight;
        }

        //Subtract weight
        public int SubWeight(Lift lift)
        {
            int newWeight = lift.Weight - 5;
            if (newWeight < 0) newWeight = 0;
            database.QueryAsync<Lift>("UPDATE LIFTS SET WEIGHT =" + newWeight.ToString() + " WHERE ID = " + lift.Id);
            return newWeight;
        }

        //Add one to reps
        public int AddRep(Lift lift)
        {
            int newReps = lift.Reps + 1;
            database.QueryAsync<Lift>("UPDATE LIFTS SET REPS =" + newReps.ToString() + " WHERE ID = " + lift.Id);
            return newReps;
        }

        //Subtract one from reps
        public int SubRep(Lift lift)
        {
            int newReps = lift.Reps - 1;
            if (newReps < 0) newReps = 0;
            database.QueryAsync<Lift>("UPDATE LIFTS SET REPS =" + newReps.ToString() + " WHERE ID = " + lift.Id);
            return newReps;
        }

        //Get Filtered Lifts
        public Task<List<Lift>> GetFilteredLifts(string muscle)
        {
            return database.QueryAsync<Lift>("SELECT * FROM LIFTS WHERE [MUSCLE] = '" + muscle + "'");
        }
    }
}
