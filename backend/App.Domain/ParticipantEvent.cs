using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class ParticipantEvent : BaseEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public bool Anonymous { get; set; }
    
    public ICollection<MinutesAdded>? MinutesAddeds { get; set; }
    public ICollection<RaffleItem>? RaffleItems { get; set; }
    
    public ActivityEvent? ActivityEvent { get; set; }
    public Guid ActivityEventId { get; set; }
}