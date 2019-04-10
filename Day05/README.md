# Day 05:  Access Level Control

## Default data in this lab
### Users
**Admin User**: admin@megsoftconsulting.com
**Password:** 12345678

### Roles
- Administrador
- Ventas
- Usuario

## Admin area with roles
We are taking the "Go to Admin area" sample to the next level. Now, We are not using cookies but instead a real authentication method to let know the user that the user must have authentication access.

The page "Admin Area" will only have the Authorize attribute set in the controller. This will allow any user authenticated in the page to access this area (which is not supposed to happen). Let's see another bad role management.

## Users controls (Role access level)
Let's use the "Control de Usuarios" page. This will tell the user that he is allowed to see the page but is supposed to be only for admin role. Surprise, by default the users are being registered as Admin. We can change this per user in the database.

Let's change a few users their roles to see their changes. 

By default, "admin@megsoftconsulting.com" has all the roles. 

## Ventas (exposing object references)
_Note: To access this page, the user must be authenticated and have either "Administrador" or "Ventas" role_

The last section is a simple Sales page (based from the Assets ownership from the first lab). This time, the user must be authenticated to see the page. In the URL route, there is the user id (used to search the Sales). 

If the user change this id to another one, the page will not restrict this and will assume that the request is being made by the owner of the assets. If the user id does not have sales, it will show a not found message. If the user does not exists, it will redirect to home.

The "Ventas a Clientes (Fixed)" page, it takes the current user logged in, with the "Ventas" role assigned to display the sales. It does not accept an ID in the route as a parameter. It will only show the current user's sales. You can use admin and ventas user for this example.