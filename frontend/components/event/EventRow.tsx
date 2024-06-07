import Link from "next/link";
import React, { useContext } from "react";
import Event from "../../entities/ActivityEvent";
import formatDate from "@/utils/dateFormater";
import { AppContext } from "../../context/StateComponent";
import { toast } from "react-toastify";

const EventRow = ({ event, index }: { event: Event; index: number }) => {
  const { events, setEvents } = useContext(AppContext);

  const handleDeleteEvent = async (e: any) => {
    e.preventDefault();
    const confirmed = window.confirm(
      "Kas sa oled kindel et soovid seda üritust kustutada?"
    );

    if (!confirmed) return;

    console.log(
      `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event/${event.id}`
    );
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1.0/Event/${event.id}`,
        {
          method: "DELETE",
          credentials: 'include'
        }
      );

      if (res.status === 204) {
        const updatedEvents = events.filter((e) => e.id !== event.id);
        toast.success("Üritus kustutatud.")
        setEvents(updatedEvents);
      } else {
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="mx-4 my-2 flex">
      <Link
        className="flex h-full flex-1 px-2 hover:border-2 hover:border-primary hover:rounded hover:box-border"
        href={`osalejad/` + event.id}
      >
        <div className="w-8/12">
          <p>
            {index + 1}. {event.name}
          </p>
        </div>
        <div className="w-2/12 flex content-center h-full">
          <p className="inline-block">{formatDate(event.startTime)}</p>
        </div>
        <div className=" flex content-center h-full ml-auto">
          <p className="inline-block ml-auto">Osavõtjad</p>
        </div>
      </Link>
      <div
        className="flex content-center h-full w-5 flex-none hover:border-2 hover:border-primary hover:rounded cursor-pointer"
        onClick={handleDeleteEvent}
      >
        <div
          className="bg-cover bg-center h-3 w-3 m-1 "
          style={{
            backgroundImage: "url('/images/remove.svg')",
            backgroundRepeat: "no-repeat",
            backgroundSize: "cover",
          }}
        ></div>
      </div>
    </div>
  );
};

export default EventRow;
