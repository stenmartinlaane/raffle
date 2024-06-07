'use client'
import React, { createContext, useContext, useEffect } from 'react';
import { Formik, Field, Form, FormikHelpers } from 'formik';
import GoogleLoginButton from './GoogleLoginButton';
import AccountService from "@/utils/AccountService";
import doesCookieExist from "@/utils/CookieHelper";
import { useRouter } from "next/navigation";
import { AppContext } from '@/context/StateComponent';

interface Values {
  email: string;
  password: string;
}
const LoginForm = () => {
    const { userInfo, setUserInfo } = useContext(AppContext);
    const router = useRouter();

    useEffect(() => {
        if (doesCookieExist("refreshTokenTimer")) {
            console.log("here")
            router.push("/");
        }
      }, []);

    const login = async (email: string, password: string) => {
        const res = await AccountService.login(email, password)
        if (await res.status < 400) {
            setUserInfo({
                email: email,
                jwt: res.jwt,
                refreshToken: res.refreshToken
            })
            router.push("/");
        }
        console.log(await res.status)
    }

  return (
    <div  className="w-4/5 h-auto p-8">
      <h2>Sisse logimine</h2>
      <Formik
        initialValues={{
          email: '',
          password: '',
        }}
        onSubmit={async (
          values: Values,
          { setSubmitting }: FormikHelpers<Values>
        ) => {
            login(values.email, values.password)
        }}
      >
        <Form>
        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="email">Email</label>
          </div>
          <div className="w-2/4">
            <Field
            className="w-full border border-black rounded px-2"
              id="email"
              name="email"
              placeholder="john@acme.com"
              type="email"
            />
          </div>
        </div>

        <div className="w-100 flex my-3">
          <div className="w-1/4">
            <label htmlFor="password">Password</label>
          </div>
          <div className="w-2/4">
            <Field className="w-full border border-black rounded px-2" suggested="current-password" type="password" id="password" name="password" placeholder="" />
          </div>
        </div>
          
          
          <div className="bg-primary p-2 rounded w-1/6">
            <button className='text-white' type="submit">Sisene</button>
          </div>
        </Form>
      </Formik>
      {/* <GoogleLoginButton></GoogleLoginButton> */}
      <button onClick={() => login("admin@eesti.ee", "Kala.maja1")}>quicklogin</button>
      <div className="bg-primary p-2 rounded w-1/6">
        <button className='text-white' onClick={() => router.push("registerUser")}>Registreeru</button>
      </div>
    </div>
  )
}

export default LoginForm