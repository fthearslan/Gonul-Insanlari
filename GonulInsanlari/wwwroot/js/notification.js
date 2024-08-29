"use strict";

function notifyError(message) {

    $.toast({
        heading: 'Error',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'error'

    });

}
function notifySuccess(message) {

    $.toast({
        heading: 'Success',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'success'

    });

}


var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").
    build();



connection.start();

connection.on("Receive", function (message) {

    notifySuccess(message);

    

});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = "It worked!";
    connection.invoke("SendNotification", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});