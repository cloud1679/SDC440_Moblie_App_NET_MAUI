using SQLite;
using Web_Service_Authentication_and_Storage.Models;

namespace Web_Service_Authentication_and_Storage.DataAccess;

public sealed class ItemRepository : IDisposable
{
    private readonly SQLiteConnection database;
    private readonly object gate = new();

    public ItemRepository(IWebHostEnvironment environment, IConfiguration configuration)
    {
        var path = configuration["DatabasePath"]
            ?? Path.Combine(environment.ContentRootPath, "App_Data", "items.db3");
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
        database = new SQLiteConnection(path);
        database.CreateTable<StoredItem>();
    }

    public List<StoredItem> GetAll()
    {
        lock (gate) return database.Table<StoredItem>().OrderBy(item => item.ItemId).ToList();
    }

    public bool TryAdd(StoredItem item)
    {
        lock (gate)
        {
            if (database.Find<StoredItem>(item.ItemId) is not null) return false;
            database.Insert(item);
            return true;
        }
    }

    public void Dispose() => database.Dispose();
}
