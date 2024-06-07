'use client'
import AccountService from '@/utils/AccountService';
import { Field, Form, Formik, FormikHelpers } from 'formik'
import { useRouter } from "next/navigation";
import React from 'react'

interface Values {
    email: string;
    password: string;
    firstname: string;
    lastname: string;
  }

  

const RegisterForm = () => {
    const router = useRouter();

    const register = async (email: string, password: string, firstname: string, lastname: string) => {
        const res = await AccountService.register(email, password, firstname, lastname)
        if (await res.status < 400) {
            router.push("/login");
        }
        console.log(await res.status)
    }

    return (
        <div className="w-4/5 h-auto p-8">
          <h1>Signup</h1>
          <Formik
            initialValues={{
              email: '',
              password: '',
              firstname: '',
              lastname: ''
            }}
            onSubmit={async (
              values: Values,
              { setSubmitting }: FormikHelpers<Values>
            ) => {
                register(values.email, values.password, values.firstname, values.lastname)
            }}
          >
            <Form>
                <div className="w-100 flex my-3">
                    <div className="w-1/4">
                        <label htmlFor="firstname">Eesnimi</label>
                    </div>
                    <div className="w-2/4">
                        <Field
                        className="w-full border border-black rounded px-2"
                        id="firstname"
                        name="firstname"
                        placeholder="john@acme.com"
                        type="firstname"
                        />
                    </div>
                </div>

                <div className="w-100 flex my-3">
                    <div className="w-1/4">
                        <label htmlFor="lastname">Perekonna nimi</label>
                    </div>
                    <div className="w-2/4">
                        <Field
                        className="w-full border border-black rounded px-2"
                        id="lastname"
                        name="lastname"
                        placeholder="john@acme.com"
                        type="lastname"
                        />
                    </div>
                </div>

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
                    <button className='text-white' type="submit">Registreeru</button>
                </div>
            </Form>
          </Formik>
          {/* <GoogleLoginButton></GoogleLoginButton> */}
          {/* <button onClick={() => login("admin@eesti.ee", "Kala.maja1")}>Login easy</button> */}
        </div>
      )
}

export default RegisterForm