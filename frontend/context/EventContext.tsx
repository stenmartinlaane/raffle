"use client";
import ActivityEvent from "@/entities/ActivityEvent";
import { createContext, useContext, useState } from "react";

type EventContextType = {
    event: ActivityEvent;
    setEvent: React.Dispatch<React.SetStateAction<ActivityEvent>>;
  };
  
  const EventContext = createContext<EventContextType | null>(null);

export function EventProvider({
    children,
  }: Readonly<{
    children: React.ReactNode;
  }>) {
  const [event, setEvent] = useState(new ActivityEvent);

  return (
    <EventContext.Provider
      value={{
        event,
        setEvent,
      }}
    >
      {children}
    </EventContext.Provider>
  );
}

export function useEventContext() {
  return useContext(EventContext);
}
