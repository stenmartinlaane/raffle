import React, { useContext, useEffect, useState } from "react";
import PersonDetail from "./person/PersonDetail";
import FirmDetail from "./firm/FirmDetail";
import { useParams } from "next/navigation";
import Spinner from "./Spinner";
import ParticipantEvent from "@/entities/ParticipantEvent";
import { useEventContext } from "@/context/EventContext";
import { AppContext } from "@/context/StateComponent";

function ParticipantDetailsForm() {
  const {event, setEvent} = useEventContext()!;
  const { id } = useParams() as { id: string };
  const [participantEvent, setParticipantEvent] = useState(() => {
    const filteredEvents = event.participantEvents.filter((pe) => pe.id === id);
    return filteredEvents.length > 0 ? filteredEvents[0] : null;
  });
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchParticipantEvent = async () => {
      try {
        const res = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/participantEvent/${id}`, {credentials: 'include'}
        );
        if (res.status === 200) {
          const data = await res.json();
          console.log(data)
          setParticipantEvent(data as ParticipantEvent);
        }
      } catch (error) {
      } finally {
      }
    };
    if (participantEvent === null) {
      fetchParticipantEvent();
    }
    setLoading(false);
  }, [id]);


  if (loading || participantEvent === null) {
    return <Spinner loading={loading} />;
  } else if (participantEvent.firm !== null) {
    return <div className="w-4/5 h-auto p-8"><FirmDetail participantEvent={participantEvent}></FirmDetail></div>;
  } else if (participantEvent.person !== null) {
    return <div className="w-4/5 h-auto p-8"><PersonDetail participantEvent={participantEvent}></PersonDetail></div>
  } else {
    return <p>{participantEvent.id}</p>
  }
}

export default ParticipantDetailsForm;
