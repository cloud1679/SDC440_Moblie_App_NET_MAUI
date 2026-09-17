namespace Local_Data_Storage_and_Access.Models
{
    public static class DatabaseConstants
    {
        public const string DatabaseFilename = "PeopleSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // Open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // Create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // Enable multi-threading
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}