using SQLite;

namespace RSVP_Project;

public class AppDatabase
{
    private SQLiteAsyncConnection? database;

    private async Task Init()
    {
        if (database != null)
            return;

        string databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "RSVPDatabase.db3");

        database = new SQLiteAsyncConnection(databasePath);

        await database.CreateTableAsync<User>();
        await database.CreateTableAsync<Event>();
        await database.CreateTableAsync<RSVP>();
    }

    public async Task<int> AddUserAsync(User user)
    {
        await Init();
        return await database!.InsertAsync(user);
    }

    public async Task<User?> GetUserByLoginAsync(
        string userName,
        string password)
    {
        await Init();

        return await database!
            .Table<User>()
            .Where(user =>
                user.UserName == userName &&
                user.Password == password)
            .FirstOrDefaultAsync();
    }
}