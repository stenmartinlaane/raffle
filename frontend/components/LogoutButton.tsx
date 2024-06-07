import React from 'react'
import AccountService from "@/utils/AccountService";
import { usePathname, useRouter } from "next/navigation";

const LogoutButton = () => {
    const router = useRouter();
    const pathname = usePathname();
    
    const logout = async () => {
       const res = await  AccountService.logout();
        if (res.status < 300) {
            router.push("/login");
        }
    }

  return (
    <div className={`${
      pathname === "/login" || pathname === "/registerUser" ? "hidden" : ""
    } px-4 h-full flex items-center hover:bg-primary hover:text-white` }>
        <button onClick={logout}>Log out</button>
    </div>
  )
}

export default LogoutButton