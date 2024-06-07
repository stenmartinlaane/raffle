import Event from "./ActivityEvent";
import ParticipantEvent from "./ParticipantEvent";


export default class MinutesAdded {
    id: string;
    date: string;
    amount: number;
    participantEventId: string;
    participantEvent: ParticipantEvent | null;

    constructor(
        id: string,
        date: string,
        amount: number,
        participantEventId: string,
        participantEvent: ParticipantEvent | null,
    ) {
        this.id = id;
        this.date = date;
        this.amount = amount;
        this.participantEventId = participantEventId;
        this.participantEvent = participantEvent;
    }
}
