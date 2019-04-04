$(document).ready(function(){
    $.post("/Home/SendData", {
        cookie: "@(Context.Request.Cookies[".AspNetCore.Identity.Application"].ToString())",
    },
        function(data, status){
            alert("Me envie");
        }
    });
});