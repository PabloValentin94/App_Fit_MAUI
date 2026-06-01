using App_Fit_MAUI.Helper;

using App_Fit_MAUI.Model;

using SQLite;

namespace App_Fit_MAUI.Helper.Modules
{
    public class Exercise : SQLite
    {
        public Exercise() : base()
        {
            if (SQLite.manager != null)
            {
                SQLite.manager.CreateTableAsync<Model.Exercise>().Wait();
            }
        }

        public Task<int> Save(Model.Exercise payload)
        {
            Task<int> rows_affected = Task.FromResult<int>(0);

            if (SQLite.manager == null)
            {
                return rows_affected;
            }

            if (payload.Id > 0)
            {
                rows_affected = SQLite.manager.UpdateAsync(payload);
            }
            else
            {
                rows_affected = SQLite.manager.InsertAsync(payload);
            }

            return rows_affected;
        }

        public Task<int> Delete(int id)
        {
            Task<int> rows_affected = Task.FromResult<int>(0);

            if (SQLite.manager == null)
            {
                return rows_affected;
            }

            rows_affected = SQLite.manager.Table<Model.Exercise>().DeleteAsync(register => register.Id == id);

            return rows_affected;
        }

        public Task<List<Model.Exercise>> Select()
        {
            if (manager == null)
            {
                return Task.FromResult<List<Model.Exercise>>(new List<Model.Exercise>());
            }

            return SQLite.manager.Table<Model.Exercise>().ToListAsync();
        }

        public Task<Model.Exercise> Find(int id)
        {
            if (SQLite.manager == null)
            {
                return Task.FromResult<Model.Exercise>(new Model.Exercise());
            }

            return SQLite.manager.Table<Model.Exercise>().FirstAsync(exercise => exercise.Id == id);
        }

        public Task<List<Model.Exercise>> Search(string query)
        {
            if (SQLite.manager == null)
            {
                return Task.FromResult<List<Model.Exercise>>(new List<Model.Exercise>());
            }

            string sql_command = $"SELECT * FROM Exercise WHERE UPPER(Description) LIKE '%{query.ToUpper()}%'";

            return SQLite.manager.QueryAsync<Model.Exercise>(sql_command);
        }
    }
}
