export const getLocalTimeZone = (cookieName) => {
    // Try get value from cookie if cookieName is provided
    const cookieValue = document.cookie
        .split(';')
        .map(c => c.trim().split('='))
        .filter(([key]) => key === cookieName)[0]?.pop();
    return cookieValue ?? Intl.DateTimeFormat().resolvedOptions().timeZone;
};
export const saveLocalTimeZone = (cookieName, timeZoneId, useSessionOnlyCookie) => {
    // Save the timezone in a cookie with the provided name.
    // persisting for infinute time if useSessionOnlyCookie is false.
    document.cookie = `${cookieName}=${timeZoneId};${useSessionOnlyCookie ? '' : 'max-age=31536000'}`;
};
