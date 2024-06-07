export default class AccountService {

    static async login(email: string, pwd: string): Promise<any> {
        const loginData = {
            email: email,
            password: pwd
        }

        const res = await fetch(
            `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/identity/Account/Login`,
            {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(loginData),
              credentials: 'include',
            }
          )
        return res;
    }

    static async register(email: string, password: string, firstname: string, lastname: string): Promise<any> {
      const registerData = {
        email: email,
        password: password,
        firstname: firstname,
        lastname: lastname
    }

      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/identity/Account/Register`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(registerData),
          credentials: 'include',
        }
      )
      return res;
    }

    static async logout(): Promise<any> {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_SERVER}/api/v1/identity/Account/LogOut`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            // 'Access-Control-Allow-Origin':`localhost:3000`,
          },
          credentials: 'include',
        }
      )
    return res;
    }

}