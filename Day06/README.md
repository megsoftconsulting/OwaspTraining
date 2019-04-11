# Day 06: Security Configuration and Sensitive Information Handling
## Live Version
https://owaspmgday06.azurewebsites.net/

## Source Control
https://dev.azure.com/megsoft/OWASP/_git/Day06

## Widgets (Bad Scenario)
In this sample, We are running a simple search using a query parameter in the URL called "categoryId" with 3 as value. The page will show an exception exposing sensitive information about the page: framework, code, etc. First, pass "x" as value and should throw invalid type value exception (it is expecting a number instead of a string). The page is telling us that it's expecting a number, so We can try passing a bigger number (30,000,000,000) and will throw another exception (to big to be an Int32).

## Widgets (Worse scenario)
In this sample, We will do the same testing as the previous page. Passing a "x" as value, it will throw an invalid type exception on the page (which now is being parsed as an Int64). If you put a bigger number, it will be parsed successfully, but, the data type in the database should be an integer, so, an exception will be thrown when the query is being executed. This time, We can see which query is being performed, information about the database, which are the column names, etc. Although is protected to SQL Injection, We can still get more information about the page.

## Widgets (Worst scenario)
In this sample, We will try the same testing as the previous page but this time someone forgot to use the configuration file for the ConnectionString value to use the SQL Connection. When you try to pass a big number and the query is executed, It will throw an exception exposing the table information, query, columns and the connection string being used to connect to the database.

## Why is this bad and how to mitigate the problem?
With this information, the attacker can gather exploits around an specific framework or look for backdoors in the system. Knowing some details of the code structure, database or sensitive information of how the page works, can give enough tools to gather even more sensitive data or exploit the site (depending on the business approach of the page, this can be dangerous).

Using connection strings directly into the commands / pages is bad practice and you put your database in a risk. To avoid this, Azure provides tools to set your configuration settings when you deploy / publish your page in a web app or using the secrets manager tool for ASP.NET Core (More information here).

For release environment, We suggest to intercept Exceptions using the middleware: 
``` csharp
IApplicationBuilder.UseExceptionHandler("{Route}")
```
Where:
Route: The user-friendly page to show when an exception happens. You can log the exception behind the scenes and check it from Azure or use you preferred exception handler (like ELMAH used to do it and now does Retrace).

Example to how to use the middleware:
``` csharp
// Check if the app is running on Development. If true, Will show the stacktrace
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Show the custom handler
    app.UseExceptionHandler("/Home/Error");
    // Enforce Hsts
    app.UseHsts();
}
```

## Man-in-the-Middle attacks
This attack consist hijack the request - response connectivity between the client and the server. The attacker sets a connection where it can read (sniff) or manipulate the data is being sent to the server and vice versa.

We can set up our own test environment to simulate an attack. We can use a software called WireShark to read the information passing through a connection. Attackers usually have a "Pineapple" router which offer the ability to provide Internet to clients, therefore, read that connection using the software.

The software provides a way to filter, set the http filter and you can see the requests coming by through your connection.

On Mac: if you have LAN connection and Wifi, you can simple Share internet through one of them and connect clients to your hotspot. More information here.

If a client is visiting your website and it's not secure, you can get sensitive date through them (like the AspNetCore Identity cookie) and you can hijack the session in your local browser.

## Example of the attack. You can see in the request the identity cookie expose.

## General recommendations around security
- Don't load login forms over HTTP, even if they post to HTTPS.
- Don't load HTTPS login forms inside an iframe on an HTTP page.
- Don't load a page over HTTP when there is no use case where it ever should.
- Don't pass sensitive data such as credentials in HTTPS addresses.

## SSL Considerations on costs
If request for a SSL / TLS certificate, awesome! But you have the following considerations:
Encrypting and decrypting traffic has to have some overhead on the server infrastructure.
It does, but it's very little according to Google after they moved GMail to HTTPS only. (Around 1% of CPU load, less than 10KB of memory per connection and less than 2% of network overhead - Google, 2010).

## Even HTTPS everywhere still has risks

- Take a common scenario:
>- User types americanexpress.com into their address bar and presses enter.
>- The browser defaults the scheme to HTTP and makes the request
>- The server responds with an HTTP 301 "Moved Permanently" with a new location of https://www.americanexpress.com
>- The browser requests the Amex website over a secure scheme.

- The second step is still vulnerable as it's an HTTP request.
- MiTM tools such as sslstrip can proxy traffic to a secure site backwards and forwards between HTTP and HTTPS.
