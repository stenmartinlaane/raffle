import React, { useState } from "react";
import ActivityEvent from "@/entities/ActivityEvent";
import { useRouter } from "next/navigation";
import { convertToISO8601 } from "@/utils/dateFormater";
import { toast } from "react-toastify";
import Link from "next/link";
import ParticipantEvent from "@/entities/ParticipantEvent";
import RaffleItem from "@/entities/RaffleItem";

function AddEventForm() {
  const router = useRouter();
  const [place, setPlace] = useState("");
  const [additionalInfo, setAdditionalInfo] = useState("");
  const [baseMinutes, setBaseMinutes] = useState(1000);
  const [minutesPerTicket, setMinutesPerTicket] = useState(10);
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [eventName, setEventName] = useState("");
  const [winnersVisible, setWinnersVisible] = useState(false);
  const [prizesVisible, setPrizesVisible] = useState(false);
  const [participantEvents, setParticipantEvents] = useState<ParticipantEvent[]>([]);
  const [raffleItems, setRaffleItems] = useState<RaffleItem[]>([]);


  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const data = new ActivityEvent(
      "00000000-0000-0000-0000-000000000000",
      baseMinutes,
      eventName,
      minutesPerTicket,
      convertToISO8601(startTime),
      convertToISO8601(endTime),
      winnersVisible,
      prizesVisible,
      participantEvents,
      raffleItems
    );
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ActivityEvent`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
          credentials: 'include',
        }
      );
      if (res.status === 201) {
        toast.success("sucess");
        router.push("/");
      } else if (res.status === 400 || res.status === 401) {
        const dataObj = await res.json();
        toast.error(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ActivityEvent`);
      } else {
        toast.error(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ActivityEvent`);
      }
    } catch (error) {
      toast.error("error");
    } finally {
    }
  };

  return (
    <div className="w-4/5 h-auto p-8">
      <h2>Ürituse lisamine</h2>
      <form className="w-1/2 py-2" onSubmit={handleSubmit}>
        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="eventName">Ürituse nimi:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2"
              type="text"
              id="eventName"
              name="eventName"
              value={eventName}
              onChange={(e) => setEventName(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="baseMinutes">Minuteid piletite kogumiseni:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2 italic"
              type="number"
              id="baseMinutes"
              name="baseMinutes"
              placeholder="pp.kk.aaaa hh:mm"
              value={baseMinutes}
              onChange={(e) => setBaseMinutes(Number(e.target.value))}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="minutesPerTicket">Mitu minutit pileti kogumiseks:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2 italic"
              type="number"
              id="minutesPerTicket"
              name="minutesPerTicket"
              placeholder="pp.kk.aaaa hh:mm"
              value={minutesPerTicket}
              onChange={(e) => setBaseMinutes(Number(e.target.value))}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="startTime">algus aeg:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2 italic"
              type="text"
              id="startTime"
              name="startTime"
              placeholder="pp.kk.aaaa hh:mm"
              value={startTime}
              onChange={(e) => setStartTime(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="endTime">Lõppu aeg:</label>
          </div>
          <div className="w-3/4">
            <input
              className="w-full border border-black rounded px-2 italic"
              type="text"
              id="endTime"
              name="endTime"
              placeholder="pp.kk.aaaa hh:mm"
              value={endTime}
              onChange={(e) => setEndTime(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="winnersVisible">Avalik võitja:</label>
          </div>
          <div className="w-3/4 h-14">
            <input
              type="checkbox"
              id="winnersVisible"
              name="winnersVisible"
              checked={winnersVisible}
              onChange={(e) => setWinnersVisible(e.target.checked)}
            />
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="prizesVisible">Avalikud auhinnad :</label>
          </div>
          <div className="w-3/4 h-14">
            <input
              type="checkbox"
              id="prizesVisible"
              name="prizesVisible"
              checked={prizesVisible}
              onChange={(e) => setPrizesVisible(e.target.checked)}
            />
          </div>
        </div>

        <div className="flex space-x-4 pt-8">
        <Link href="/">
            <div className="bg-secondary p-2 rounded">
              <button>Tagasi</button>
            </div>
          </Link>
          <div className="bg-primary p-2 rounded">
            <button type="submit">Lisa</button>
          </div>
        </div>
      </form>
    </div>
  );
}

export default AddEventForm;
