using SQLite;

namespace RSVP_Project;

public class RSVP
{
    [PrimaryKey, AutoIncrement]
    public int RSVPId { get; set; }

    public int EventId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}