import ParticipantEvent from "./ParticipantEvent";
import RaffleItem from "./RaffleItem";

export default class ActivityEvent {
  id: string;
  baseMinutes: number;
  eventName: string;
  minutesPerTicket: number;
  startTime: string;
  endTime: string;
  winnersVisible: boolean;
  prizesVisible: boolean;
  participantEvents: ParticipantEvent[];
  raffleItems: RaffleItem[];

  constructor(
    id?: string,
    baseMinutes?: number,
    eventName?: string,
    minutesPerTicket?: number,
    startTime?: string,
    endTime?: string,
    winnersVisible?: boolean,
    prizesVisible?: boolean,
    participantEvents?: ParticipantEvent[],
    raffleItems?: RaffleItem[]
  ) {
    if (
      id &&
      baseMinutes !== undefined &&
      eventName &&
      minutesPerTicket !== undefined &&
      startTime &&
      endTime &&
      winnersVisible !== undefined &&
      prizesVisible !== undefined &&
      participantEvents &&
      raffleItems
    ) {
      this.id = id;
      this.baseMinutes = baseMinutes;
      this.eventName = eventName;
      this.minutesPerTicket = minutesPerTicket;
      this.startTime = startTime;
      this.endTime = endTime;
      this.winnersVisible = winnersVisible;
      this.prizesVisible = prizesVisible;
      this.participantEvents = participantEvents;
      this.raffleItems = raffleItems;
    } else {
      this.id = "";
      this.baseMinutes = 0;
      this.eventName = "";
      this.minutesPerTicket = 0;
      this.startTime = "";
      this.endTime = "";
      this.winnersVisible = false;
      this.prizesVisible = false;
      this.participantEvents = [];
      this.raffleItems = [];
    }
  }
}
