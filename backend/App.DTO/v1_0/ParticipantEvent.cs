using Base.Domain;

namespace App.DTO.v1_0;

public class ParticipantEvent : BaseEntityId
{
    public bool Anonymous { get; set; }
    
    public ICollection<MinutesAdded>? MinutesAddeds { get; set; }
    public ICollection<RaffleItem>? RaffleItems { get; set; }
    
    public ActivityEvent? ActivityEvent { get; set; }
    public Guid ActivityEventId { get; set; }
}