# Day 04: Prevent poor session handling
## Sensitive data in cookies
First, go to the "Go To Admin" option in the navigation bar. To access the Admin area, you need to have the "IsAdmin" permission. The previous developer, to make things easier, it sets a flag in the authenticated user that says "IsAdmin" in the session information. 

If you go to the admin area, the page will return a 401 error. Go back and open up the developer tools of your browser. In the Storage information, identify the cookie that is named "IsAdmin" and set the value from 0 to 1.

Try to get access again to the admin area and you can finally reach it because now you are an admin. As a plus, you can see the banking accounts of the administrators totally exposed with not protection at all.

## Session Hijacking
_Notes: For this sample you need two browsers, to simulate the attacked user and the attacker. We recommend use Firefox since Chrome blocks any kind of hijacking on session information_.

First, You need to register a new user using the "Register" option in the top nav bar menu on the page. You can simple add a user called "newuser@me.com" with a simple password. 

_Note: This sample is running Asp.Net Core Identity Framework architecture with the lowest capable configuration (which you can find the in the Startup.cs file)._

When the user is finally register, the page will generate a session Id which identifies the current user's session. Go to the developers tool of your current browser and look for the the following cookie: .AspNetCore.Identity.Application.

Open another browser and go to the application url. You should not have an active session. Open the developer tools of that browser and proceed to insert a new cookie with the same name and value We got before. After setting the cookie, let's refresh the page in the unauthenticated page. If everything is ok, the cookie should be accepted by the server and will login with the same user We previously created.

## What's happening and why is this a risk?
Imagine that you are an account manager and you have access to sensitive data from your users. You leave your session open in a computer, your office computer or even in your home. The attacker can inject an script (using XSS) to get your session id and impersonate you from a remote computer or even subtract it from your own computer (in a spy-like fashion way) while you are in the bathroom.

## XSS information gathering
Our Lab site has the HttpOnly policy to none. This means that We can inject a script that gather the user's session information. Insert this script in the comment input in the XSS page:

``` html
<script>function getCookieData(name) { var pairs = document.cookie.split("; "), count = pairs.length, parts; while ( count-- ) { parts = pairs[count].split("="); if ( parts[0] === name ) return parts[1]; } return false;} alert("Cookie: " + getCookieData(".AspNetCore.Identity.Application"));</script>
```

The information is now expose through an script injected into the site. The idea here is to show how important is to check security policies at the Startup process. We can avoid this script if We turn on HttpOnly policy in Startup.cs.