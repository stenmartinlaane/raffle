import MinutesAdded from "./MinutesAdded";
import Event from "./ActivityEvent";
import ActivityEvent from "./ActivityEvent";


export default class RaffleItem {
    id: string;
    itemName: string;
    activityEvent: ActivityEvent |null;
    activityEventId: string;
    participantEvent: Event | null;
    participantEventId: string;

    constructor(
        id: string,
        itemName: string,
        activityEvent: ActivityEvent | null,
        activityEventId: string,
        participantEvent: Event | null,
        participantEventId: string
    ) {
        this.id = id;
        this.itemName = itemName;
        this.activityEvent = activityEvent;
        this.activityEventId = activityEventId;
        this.participantEvent = participantEvent;
        this.participantEventId = participantEventId;
    }
}
