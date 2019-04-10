# Day 02: Cryptography
## Passwords
The password sample shows you two grids with User and Password information: one using MD5 hash method and the other Base64 string passwords (this is the worse scenario). 

## Steps to show this sample
Pick one of the MD5 password and open up the MD5 Generator website and decrypt the password. The website will run its collision method or check in their database if that hash is already broken. The page will show the decrypted password on screen. MD5 is not secure.

### MD5 hash password in the example
Hash the following passowrds using MD5 and try to de-hash them in the website [MD5 Online](https://www.md5online.org/):
- admin: ThisIsMyPassword
- carlos: mypasswordissecure
- enmanuel: MyOtherSecurePassword123 

For Base64 strings open up [base64encode](https://www.base64encode.org/) to encode passwords and [base64decode](https://www.base64decode.org/) website to decode passwords with Base64 strings. Base64 is less secure (is not cryptography at all).

## Query Strings
Steps to show this sample
This is a simple Login form that submits the information through query string. The page will show the query string at the end of the page. Also, you can check in the URL the sensitive information.

## More about Query Strings
You can follow up the query strings topic with this sample: this page shows products for an specific owner. The idea is to show how easy should be to show information that is not supposed to be shown to specific users. For example: You are the Owner of the products listed in the sample but you want to see other products for other owners; in the URL, the user can see the pattern of the search: a Query String exposing how the query is being perform to the database. Setting the the OwnerId = 2, you will see the Products assign to other user.

## Expose credentials in Source Control
There is no need to show a sample on the website but the Day's repo. The connection that is used live is set in the appsettings.json file.

To avoid this, you can set the ConnectionString in your Azure Web App (Settings Section). If you set "DefaultConnection" on Azure, it will override the value that is set in the appsettings.json file. If there is not DefaultConnection value, it will be set.

_Note: DefaultConnection is not a mandatory name (in fact, is recommended to change this name to another name) because "DefaultConnection" is the Default name for any ASP.NET application (which implies a risk itself) _