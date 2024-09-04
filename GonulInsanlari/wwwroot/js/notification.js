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

        connection.on("Receive", function (message) {

            notifySuccess(message);

        });

    

