using Base.Domain;

namespace App.Domain;

public class RaffleItem : BaseEntityId
{
    public string ItemName { get; set; } = "";
    
    public ActivityEvent? ActivityEvent { get; set; }
    public Guid ActivityEventId { get; set; }
    
    public ParticipantEvent? ParticipantEvent {get; set; }
    public Guid ParticipantEventId { get; set; }
}