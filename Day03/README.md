# Day 03: Cross Site Scripting attacks

## XSS
This sample consist in a simple user input that allow us to send a comment via POST and show it in the same page.

Steps to Run the sample and how to exploit it?
Insert any text in the comment input area and submit the data. The controller will return the data sent through post (the same text in the input). After that, insert the following text:

``` html 
<script>alert("Hello world");</script>
```

When you submit this information, the Script will be executed when the page is reloaded and inserting the Raw html in the document. This Script will only show an alert with the message "Hello world". Try different messages at this point.

## Taking the exploit even further
Run this script in the comment input and submit it:
``` html
1 2 3 4 5 <style> .container, .navbar{display:none;}  body { padding: 5px }  body:before { content:"This site is hacked. Head over https://myhackersite.xyz to continue"; } </style>
```
This will insert the style script into the page and replace the whole document with a new body content.

Run this script in the comment input and submit it:
``` html
<script>window.location.replace("https://www.google.com");</script>
```

Run this script in the comment input and submit it:

``` html
<meta http-equiv="refresh" content="0;">
```

This will refresh the page every 0.3s to the XSS sample page. It then act as an infinite loop of refresh requests, potentially bringing down the web and database server by flooding it with requests. The more browser sessions that are open, the more intense the attack becomes.

## Why is this happening?
XSS are attacks made by three components: the attacker, a client and the web site. The script injected into the page when the web site or the client does not validate the text inserted into the page. When inputs are not validated before saving the data or displaying information into the page (looking for malicious code), allows the attacker to manipulate the DOM and even hijack information about the user.

## How to solve it?
Never put untrusted data into your HTML input, unless you follow the rest of the steps below. Untrusted data is any data that may be controlled by an attacker, HTML form inputs, query strings, HTTP headers, even data sourced from a database as an attacker may be able to breach your database even if they cannot breach your application.

Before putting untrusted data inside an HTML element ensure it's HTML encoded. HTML encoding takes characters such as "<" and changes them into safe form like "%lt;".
Before putting untrusted data into an HTML attribute ensure it's HTML encoded. HTML attribute encoding is a superset of HTML encoding and encodes additional characters such as " and '.

Before putting untrusted data into JavaScript place the data in an HTML element whose contents you retrieve at runtime. If this isn't possible, then ensure the data is JavaScript encoding takes dangerous characters for JavaScript and replaces them with their hex, for Example < would be encoded  as "\u003C".
Before putting untrusted data into a URL query string ensure it's URL encoded.
Encode HTML using Razor engine (link).