using SQLite;

namespace App_Fit_MAUI.Helper
{
    public abstract class SQLite
    {
        public static SQLiteAsyncConnection? manager = null;

        public SQLite()
        {
            if (manager == null)
            {
                string database_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

                manager = new SQLiteAsyncConnection(database_file_path);

                System.Diagnostics.Debug.WriteLine($"Arquivo do SQLite (Caminho): {database_file_path}");
            }
        }

        public async Task DiscardConnection()
        {
            manager = null;
        }
    }
}
