using SQLite;

namespace Web_Service_Authentication_and_Storage.Models;

public class StoredItem
{
    [PrimaryKey]
    public string ItemId { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string ItemDescription { get; set; } = "";
}
