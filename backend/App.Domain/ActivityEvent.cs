using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class ActivityEvent : BaseEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int BaseMinutes { get; set; }
    public string EventName { get; set; } = "";
    public int MinutesPerTicket { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool WinnersVisible { get; set; }
    public bool PrizesVisible { get; set; }
    
    public ICollection<ParticipantEvent>? ParticipantEvents { get; set; }
    public ICollection<RaffleItem>? RaffleItems { get; set; }
}