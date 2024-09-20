function notify(title,message,icon) {

    $.toast({
        heading: title,
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: icon

    });

}

function notifyError(message) {

    $.toast({
        heading: 'Error',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon:'error'

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
function notifyWarning(message) {

    $.toast({
        heading: 'Warning',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'warning'

    });

}




