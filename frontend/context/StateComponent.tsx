"use client";

import { useEffect, useState } from "react";
import { createContext } from "react";
import ActivityEvent from "@/entities/ActivityEvent";
import { EventProvider } from "./EventContext";
import LoginResponse from "@/entities/LoginResponse";

export default function StateComponent({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [events, setEvents] = useState(Array<ActivityEvent>);
  const [userInfo, setUserInfo] = useState<IUserInfo | null>(null);
  const [jwtCookieExpireTimeInMinutes, setJwtCookieExpireTimeInMinutes] = useState(5);

  const fetchJwtCookie = async () => {
    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/identity/Account/RefreshJwt`, {
        method: 'GET',
        headers: {
          "Accept": "application/json",
        },
        credentials: 'include',
      });
      if (response.ok) {
        let loginResponse = await response.json()
        setJwtCookieExpireTimeInMinutes(Number(loginResponse.jwtCookieExpireTimeInMinutes));
      } else {
        console.error('Failed to fetch JWT token');
      }
    } catch (error) {
      console.error('Error fetching JWT token:', error);
    }
  };

  useEffect(() => {
    fetchJwtCookie();
    const intervalId = setInterval(fetchJwtCookie, jwtCookieExpireTimeInMinutes * 60 * 1000 - 30 * 1000);
    return () => clearInterval(intervalId);
  }, [jwtCookieExpireTimeInMinutes])

  return (
    <AppContext.Provider
      value={{ events, setEvents, userInfo, setUserInfo }}
    >
      <EventProvider>
        {children}
      </EventProvider>
    </AppContext.Provider>
  );
}

export interface IUserInfo {
  "jwt": string,
  "refreshToken": string,
  "email": string
}

interface IAppContext {
  events: ActivityEvent[];
  setEvents: (val: ActivityEvent[]) => void;
  userInfo: IUserInfo | null,
  setUserInfo: (userInfo: IUserInfo | null) => void
}

export const AppContext = createContext<IAppContext>({
  events: [],
  setEvents: () => {},
  userInfo: null,
  setUserInfo: () => {}
});
