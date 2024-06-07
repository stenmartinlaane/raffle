export default function doesCookieExist(name : string) {
    if (document.cookie.indexOf(name) != -1) {
        return true;
    }
    return false;
}