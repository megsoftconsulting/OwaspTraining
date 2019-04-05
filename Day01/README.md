# Day 01 References: SQL Injection

## Login

This sample consists to show attendants how simple is to access a vulnerable login process using SQL Injection.

### Steps to Run the sample

Login using the following user credentials:

- admin -> 12345
- enmanuel -> 123456
- carlos -> 987456
When using these credentials, it shows a "Login Successful" message at the end of the page. If you use another credential, it will fail.

### How to exploit the sample?

Use a user (for example "admin") and go to the password section. Instead of using the correspondent password, insert the following one:

> ' OR 1=1;--

If using correctly, the Success message should be displayed at the end of the page.

### Why is this happening?

The sample is running a SqlCommand with no parameter being set or any string sanitation is being running before executing the command. The query being used to check the user identity is:

>var sqlString = $"SELECT * FROM Users WHERE Username='{username}' AND password='{password}';"

When you send ' OR 1=1;-- as password the query being run in the database is as follows:

>SELECT * FROM Users WHERE Username='admin' AND '' OR 1 = 1;--';

Where setting a new condition in the WHERE statement which will always return a result, the -- characters will opt-out the '; characters which make the query valid.

### How to fix it? (Login (Secure) Sample)

Run again the same steps to exploit the login using the Login (Secure) sample, this time performing SQL Injection will not work returning a "Fail" message. This sample is using SqlParameters to check if a parameter is correctly being set in the query before running it. The string which is being used to the SqlCommand is set as follows:

> var sqlString = $"SELECT * FROM Users WHERE Username=@username AND Password=@password;"

In this way, when the SqlCommand checks for the parameters and detects an invalid parameter (a.k.a performing SQL Injection) it will fail.

## Products

This sample is a simple grid showing the existing products in a table. The idea is to filter by name and return results which match the parameter, which We can exploit to show more information that is supposed to be shown [Insert reference here].

How to exploit the sample?
Set the following text on the search field:

> mac%' UNION SELECT Username, Password FROM USERS;--

This will return the filtered results and also the Username and Password information as part of the results.

### Why this is happening?

As the previous example, the query being used is as follows:

>var sqlString = $"SELECT * FROM Products WHERE Name LIKE '%{filter}%';"

This query allow us to attached a UNION statement based on what We learned in the Login sample. With mac%' UNION SELECT Username, Password FROM USERS;-- We are setting a new query which will look like this:

>SELECT * FROM Products WHERE Name LIKE '%mac%' UNION SELECT Username, Password FROM USERS;--'%

This query attached the results on the next SELECT to the previous one and the two fields are being shown as Name and Type which are the fields for the Products table. Also the '% are being opt-out thanks to the -- characters.

### How to fix it (Products (Fixed) sample)

As the Login (Fixed) sample, the best thing to do is to set a SqlParameter to the query and allow the SqlCommand to check it before is being executed in the server. The new query will be like this:

>var sqlString = $"SELECT * FROM Products WHERE Name LIKE '%' + @filter +'%';"

Repeat the steps to inject the UNION statement in the search field and this will not return any results.

## Doomsday sample

The "Doomsday" scenario is to inject DDL commands into querier where you shouldn't have to be capable to. For this, We are using the same Login sample.

### How to exploit the sample?

Use a user (for example "admin") and go to the password section. Instead of using the correspondent password, insert the following one:

>' OR 1=1; CREATE TABLE TEST(TestColumn nvarchar(MAX));--

This las command will show a Success login and also create a table in the database called TEST. You can open the database and show that the TEST table has been created. After this, you can drop down that table using the same pattern: ' OR 1=1; DROP TABLE TEST;--

### Why this is happening?

This is mainly happening because the credentials being used to connect to the database may have dbowner or sysadmin permissions. This will allow to alter the database schema through not secured inputs (like the Login Page).

### How to solve this (Doomsday sample)?

By creating a new user with limited permissions will stop attackers to inject DDL commands in the queries. The fixed sample is using a different ConnectionString which is using credentials with read-only permissions. This will stop attackers to modify the schema in our database.