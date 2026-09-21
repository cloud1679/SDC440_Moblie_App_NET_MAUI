using SQLite;

namespace Store_and_Retrieve_Date_SQLite.Models
{
    public class Item
    {
        [PrimaryKey]
        public int ItemID { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string ItemDescription { get; set; } = string.Empty;
    }
}