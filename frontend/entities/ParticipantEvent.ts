import MinutesAdded from "./MinutesAdded";
import RaffleItem from "./RaffleItem";
import ActivityEvent from "./ActivityEvent";


export default class ParticipantEvent {
    id: string;
    anonymous: boolean;
    minutesAddeds: MinutesAdded[];
    raffleItems: RaffleItem[];
    activityEvent: ActivityEvent | null;
    activityEventId: string

    constructor(
        id: string,
        anonymous: boolean,
        minutesAddeds: MinutesAdded[],
        raffleItems: RaffleItem[],
        activityEvent: ActivityEvent | null,
        activityEventId: string,
    ) {
        this.id = id;
        this.anonymous = anonymous;
        this.minutesAddeds = minutesAddeds;
        this.raffleItems = raffleItems;
        this.activityEvent = activityEvent;
        this.activityEventId = activityEventId
    }
}
