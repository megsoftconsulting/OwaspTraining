# Day 08: Validate Redirects and Forwards
## Lab 
### Links
In this sample We have three simple links to external websites. This links have nothing in particular. When you follow the links, the page show a redirect page before send the user to site that clicked. Now, the attacker can take advantage of this and gran our site as a launchpad for an attack (phishing our identity). Copy and paste this link into the browser:

```
https://owaspmgday08.azurewebsites.net/Home/RedirectUrl?site=https%3A%2F%2Fdownload.microsoft.com%2Fdownload%2F9%2FA%2F8%2F9A8FCFAA-78A0-49F5-8C8E-4EAE185F515C%2FWindows6.1-KB917607-x86.msu
```

This link, sends the user to a download website from Microsoft and downloads the software to the computer.

### Why this is a risk?
Although is a Microsoft software (for the purpose of this sample), an attacker can obfuscate even better the site value in the URL and send us to `evilsite.com/malware.exe`. Usually users trust our site and attackers can take opportunity on this trust to send malware through a website, social media, email, etc.

## Solutions
### Whitelist your URLs
One solution is to whitelist your URL in a database. In the second sample "Links (Usando Whitelist)", you can check that the links We previously provided are going to work during the redirect; but if the user uses an alternative link in the redirect, it will throw an error because it will not be found in the database.

_Note: Although this is a good solution, it has a big issue: you have to register every link in your website into this database (which can be annoyed), imagine you have to enter 300 links in your site!_

### Referrers
To avoid this, you can check the Referrer alternative: this consist and get the referrer value in the Header of an Action call request. Comparing the referrer value: either it's null or if it's different from the Action's Host information you can know if the URL is a valid URL in your site or if it's someone trying to impersonate your site (an attacker).

In the last example, you can see how only the links that are printed in the page are going to work. If the users put manually the url in the browser or even using the alternative one, this is going to fail because putting the URL in the browser will not send the Referrer header and only links on your website will work.

The code used for this example is as follows:
``` csharp
try 
{
    if (string.IsNullOrEmpty (site)) return RedirectToAction ("Index");

    // Check if the Referer is in the header, otherwise, return Error action
    var referrer = Request.Headers["Referer"].ToString ();
    if (string.IsNullOrEmpty (referrer)) return RedirectToAction ("Error", new { message = referrer });

    // Check if the Referer URL is coming from the out Host, otherwise, return Error action
    var referrerUrl = new Uri (referrer);
    if (referrerUrl.Host != Request.Host.ToString ())
        return RedirectToAction ("Error", new { message = $"Referer = {referrerUrl.ToString()}; Host: {Request.Host.ToString()}; Referer Host: {referrerUrl.Host}" });

    // Check if the URL is on the whitelist, otherwise, return Error action
    if (!_context.TrustedUrls.Any (x => x.Name == site)) return RedirectToAction ("Error");
} 
catch (Exception e) 
{
    Console.WriteLine (e);
    return RedirectToAction ("Error", new { message = e.Message });
}

// TODO If link is trusted, do your Log here
// Show the redirect site ...
ViewData.Add ("url", site);
return View ();
```

This is a great solution and even you can combine both approaches for this. Unfortunately, some browsers may or may not use the Referrer value when the user sends s Request to the server (this will totally depend of the client your users are using). We suggest if you are going to use this approach, test this approach on every browser you can test.

## Is this totally worth it?
There is a debate around this risk in the community, even Google considers this a low security risk because involves to many scenarios to try to cover up. Even if you report an issue regarding of this "risk", they are not going to fix it. The impact of the risk is affecting the reputation of your website: attackers can impersonate you and send a link using your website as a launchpad and then perform an attack but that's it. Not data it's expose in the website nor security flaws of your system. But, you can loose the trust you have gain from your users depending of the severity of the attacks being use in the name of your site. 