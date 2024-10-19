"use strict";
((doc) => {
    // If the user agent is a bot, do nothing. (https://stackoverflow.com/a/20084661/1268000)
    if (/bot|crawl|spider/i.test(navigator.userAgent))
        return;
    // Get the cookie name from the script tag which is the last script tag in the document.
    const scriptTag = doc.currentScript;
    const cookieName = scriptTag?.getAttribute('cookie-name') ?? "tz";
    // Try to get the cookie value
    const cookieValue = doc.cookie
        .split(';')
        .map(c => c.trim().split('='))
        .filter(([key]) => key === cookieName)[0]?.pop();
    // If the cookie value is not found, get the timezone from the browser, save it in a cookie, and reload the page.
    if (!cookieValue) {
        const timeZoneId = Intl.DateTimeFormat().resolvedOptions().timeZone;
        doc.cookie = `${cookieName}=${timeZoneId}`;
        location.reload();
    }
})(document);
