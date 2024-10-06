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
        heading: 'New Notification',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'success',
        

    });

}
function notify(title, message, icon) {

    $.toast({
        heading: title,
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: icon

    });

}

function markAsSeen(id) {

    $.ajax({
        type: "POST",
        data: { notificationId: id },
        url: "/notifications/markAsSeen",
        success: function () {

        }
    });
}


var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").
            build();



        connection.start();

        

connection.on("Notify", function (notification) {

    notify(notification.title, notification.content, notification.resultType)

});


