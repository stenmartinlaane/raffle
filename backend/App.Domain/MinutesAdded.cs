using Base.Domain;

namespace App.Domain;

public class MinutesAdded : BaseEntityId
{
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public Guid ParticipantEventId { get; set; }
    public ParticipantEvent? ParticipantEvent { get; set; }
}