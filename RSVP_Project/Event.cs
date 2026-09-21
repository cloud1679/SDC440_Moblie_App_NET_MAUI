using SQLite;

namespace RSVP_Project;

public class Event
{
    [PrimaryKey, AutoIncrement]
    public int EventId { get; set; }

    public int HostUserId { get; set; }

    public string EventName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int MaxAttendees { get; set; }

    public DateTime EventDateTime { get; set; }

    public DateTime RSVPDeadline { get; set; }
}