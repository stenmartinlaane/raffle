import React, { useContext, useEffect, useState } from "react";
import EventInfo from "./event/EventInfo";
import EventRow from "./event/EventRow";
import ActivityEvent from "@/entities/ActivityEvent";
import Spinner from "@/components/Spinner";
import { AppContext } from "../context/StateComponent";

const HomePageContent = () => {
  const { events, setEvents } = useContext(AppContext);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const res = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/ActivityEvent`,{
            method: "GET",
            credentials: 'include',
          }
        );

        if (res.status === 200) {
          const data = await res.json();
          setEvents(data);
        }
      } catch (error) {
        console.log(error);
      } finally {
        setLoading(false);
      }
    };
    fetchEvents();
  }, []);

  return (
    <div className="flex flex-col w-full h-full">
      <div className="h-1/2 w-full flex flex-row mb-4 flex-1">
        <div
          className="bg-primary h-full flex justify-center items-center p-10"
          style={{ width: "calc(50% - 0.5rem)" }}
        >
          <p className="text-white font-light">
            Sed nec elit vestibulum,{" "}
            <span className="text-white font-semibold">tincidunt orci</span> et,
            sagittis ex. Vestibulum rutrum{" "}
            <span className="text-white font-semibold">neque suscipit</span>{" "}
            ante mattis maximus. Nulla non sapien{" "}
            <span className="text-white font-semibold">
              viverra, lobortis lorem non
            </span>
            , accumsan metus.
          </p>
        </div>
        <div
          className="h-full"
          style={{
            backgroundImage: "url('/images/pilt.jpg')",
            backgroundRepeat: "no-repeat",
            backgroundSize: "cover",
            width: "calc(50% + 0.5rem)",
          }}
        ></div>
      </div>
      {loading ? (
        <Spinner loading={loading} />
      ) : (
        <div className="flex flex-row space-x-4 w-full h-full flex-1">
          <EventInfo title="Tulevased Üritused">
            {events
              .filter((event: ActivityEvent) => new Date(event.startTime) > new Date())
              .map((event: ActivityEvent, index: number) => (
                <EventRow key={index} event={event} index={index}></EventRow>
              ))}
          </EventInfo>
          <EventInfo title="Toimunud Üritused">
            {events
              .filter((event: ActivityEvent) => new Date(event.startTime) <= new Date())
              .map((event: ActivityEvent, index: number) => (
                <EventRow key={index} event={event} index={index}></EventRow>
              ))}
          </EventInfo>
        </div>
      )}
    </div>
  );
};

export default HomePageContent;
