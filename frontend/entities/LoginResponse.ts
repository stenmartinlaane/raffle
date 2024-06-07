export default class LoginResponse {
    jwtCookieExpireTimeInMinutes: number;

    constructor(
        jwtCookieExpireTimeInMinutes: number,
    ) {
        this.jwtCookieExpireTimeInMinutes = jwtCookieExpireTimeInMinutes;
    }
}