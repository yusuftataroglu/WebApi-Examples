$(document).ready(function(){
    
})

// function CheckUserInfo(){
//     var localUserName = localStorage.getItem("userName");
//     var localPassword = localStorage.getItem("password");

//     if(localUserName != null && localPassword != null){
//         $.ajax({
//             type: "Get",
//             url: "https://localhost:7110/api/gameaccess",
//             headers:{
//                 Authorization:"Basic " + btoa(localUserName+":"+localPassword)
//             },
//             success: function (response) {
//                 window.location.href="/game.html";
//             },
//             error: function(err){
//             }
//         });
//     }
// }

$("#loginBtn").click(function (e) {
    var userName = $("#userName").val();
    var password = $("#password").val();

    $.ajax({
        url: "https://localhost:7110/api/gameaccess",
        type: "Get",
        headers: {
            Authorization: "Basic " + btoa(userName + ":" + password)
        },
        success: function (result) {
            localStorage.setItem("userName", userName);
            localStorage.setItem("password", password);
            window.location.href="/game.html";
        },
        error: function (err) {
            $("#messageDiv").css("display","block").text("Bir hata oluştu");
        }
    })
})