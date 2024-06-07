using Base.Domain;

namespace App.DTO.v1_0;

public class ActivityEvent : BaseEntityId
{
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