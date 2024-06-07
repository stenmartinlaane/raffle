"use client";
import Link from "next/link";
import { usePathname } from "next/navigation";
import LogoutButton from "../LogoutButton";
import { useState } from "react";

export default function NavBar() {
  const pathname = usePathname();
  const [selectedValue, setSelectedValue] = useState("Participant")

  return (
    <>
      <header
        className="relative flex w-full flex-wrap items-center justify-between bg-white-background
            flex-shrink-0 flex-none flexBetween"
      >
        <nav className="h-full w-full flex flex-1">
          <ul className="list-style-none flex ps-0 flex-row h-full w-full">
            <Link href="/" className="w-1/5 flex items-center pl-6">
              <img className="w-24" src="/images/logo.svg"></img>
            </Link>
            <Link
              href="/"
              className={`${
                pathname === "/" ? "bg-primary text-white" : ""
              } px-4 h-full flex items-center hover:bg-primary mr-1 hover:text-white`}
            >
              AVALEHT
            </Link>
            <Link
              href="/lisa-yritus"
              className={`${
                pathname === "/lisa-yritus" ? "bg-primary text-white" : ""
              } px-4 h-full flex items-center hover:bg-primary hover:text-white` }
            >
              ÜRITUSE LISAMINE
            </Link>
            <div>
            <label htmlFor="dropdown">Roll :</label>
            <select id="dropdown" value={selectedValue} onChange={(e) => {setSelectedValue(e.target.value)}}>
              <option value="Participant">Osaleja</option>
              <option value="organizer">Korraldaja</option>
            </select>
          </div>
            <div className="px-8 py-1 ml-auto">
              <LogoutButton></LogoutButton>
              {/* <img src="/images/symbol.svg"></img> */}
            </div>
          </ul>
        </nav>
      </header>
    </>
  );
}
