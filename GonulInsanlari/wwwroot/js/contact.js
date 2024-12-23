function notify(title, message, icon) {

    $.toast({
        heading: title,
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: icon

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
function notifyError(message) {

    $.toast({
        heading: 'Error',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'error'

    });

}


function refresh(status) {

    $('#mails').empty();




    $.ajax({
        type: "POST",
        url: "/mail/refresh",
        data: { status: status },
        success: function (result) {


            for (let i = 0; i < result.length; i++) {

                var cls = "";
                var icon = "";

                if (result[i].isSeen) {
                    cls = "read";

                } else {
                    cls = "unread";
                    icon = "fa fa-eye";

                }

               let fromOrTo = result[i].from;

                switch (result[i].contactStatus) {

                    case 1:

                        fromOrTo = result[i].from;
                        break;

                    default:
                        fromOrTo = result[i].to;
                        cls = "read";
                        icon = "";

                }

                var html = '<tr id="' + result[i].id + '" class="' + cls + '" style="cursor:pointer">' +
                    '<td class="check-mail">' +
                    '<input type="checkbox" class="i-checks" value="' + result[i].id + '">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-ontact">' + fromOrTo + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-subject">' + result[i].subject + '<a href="mail/detail/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + icon + ' ' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id + ')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';



                $("#mails").append(html);


            }

        }
    });
};


function Send(id) {


    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert it...",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/mail//" + id,
                success: function () {

                    window.location.replace("/mail/inbox");
                }


            }
            );


        }


    });



    /*  window.location = "/Admin/Contact/GetDetails/" + id;*/
    window.location = "/mail/detail/" + id;
}

function markAsRead() {

    var allVals = [];
    $('input[type="checkbox"]:checked').each(function () {
        allVals.push($(this).val());
    });


    $.ajax({
        type: "POST",
        data: { ids: allVals },
        url: "/mail/markasread",
        success: function (result) {
            if (result.success) {

                notifySuccess(result.responseMessage);

                for (let i = 0; i < allVals.length; i++) {

                    $("." + allVals[i]).hide();



                }

            }
            else {

                notifyError(result.responseMessage);
            }

        }

    }
    );



}
function Delete() {

    var allVals = [];
    $('input[type="checkbox"]:checked').each(function () {
        allVals.push($(this).val());
    });


    $.ajax({
        type: "POST",
        data: { ids: allVals },
        url: "/mail/delete",
        success: function (result) {
            if (result.success) {

                notifySuccess(result.responseMessage);
             

                for (let i = 0; i < allVals.length; i++) {

                    $("#" + allVals[i]).hide();



                }


            }
            else {
                notifyError(result.responseMessage);
               
            }

        },
        statusCode: {
            403: function () {

                notify('Access denied!', 'You do not have an access to delete contacts.', 'error');
              
            }

        }

    }
    );



}

function DeleteSingle(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert it...",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/mail/delete/" + id,
                success: function () {

                    window.location.replace("/mail/inbox");
                }


            }
            );


        }


    });



}

// New timeout variable
let timeout = null;
document.getElementById('searchbar').addEventListener('keyup', function (e) {


    // Clear existing timeout      
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {


        search();

    }, 500);

});

function search() {

    $('#mails').empty();

    var model = {

        Search: document.getElementById('searchbar').value,
        ContactStatus: $('#contactStatus').val(),
        GetAll: $('#getAll').val(),

    };

    $.ajax({
        type: "POST",
        url: "/mail/search",
        data: { model: model },
        success: function (result) {

            for (let i = 0; i < result.length; i++) {


                var cls = "";
                var icon = "";

                if (result[i].isSeen) {
                    cls = "read";

                } else {
                    cls = "unread";
                    icon = "fa fa-eye";

                }

                fromOrTo = result[i].from;

                switch (result[i].contactStatus) {

                    case "Received":

                        fromOrTo = result[i].from;
                        break;

                    default:
                        fromOrTo = result[i].to;
                        cls = "read";
                        icon = "";

                }

                var html = '<tr id="' + result[i].id + '" class="' + cls + '" style="cursor:pointer">' +
                    '<td class="check-mail">' +
                    '<input type="checkbox" class="i-checks" value="' + result[i].id + '">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-ontact">' + fromOrTo + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-subject">' + result[i].subject + '<a href="/Admin/Contact/GetDetails/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + icon + ' ' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id + ')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';

                $("#mails").append(html);


            }

        }
    });





}



function uploadFiles(inputId) {


    var input = document.getElementById(inputId);
    var files = input.files;

    for (var i = 0; i != files.length; i++) {

        let fileName = files[i].name.replace(" ", "");
        var index = Math.floor(Math.random() * 100);

        var html = '<div id="file+' + index + '" style="margin-right:auto;margin-left:10px;" class="file-box">' +


            '<div class="file">' +
            '<div onclick="hide(' + index + ')" style="cursor:pointer">' +
            '<i class="fa fa-times"></i>' +
            '</div>' +
            '<span class="corner"></span>' +
            '<div class="icon">' +
            '<i class="fa fa-file"></i>' +
            '</div>' +
            '<div class="file-name">' +
            '<a href="/contact/download/file/' + fileName + '">' +
            fileName
        '</a>' +

            '</div>' +
            '</div>' +
            '</div>';


        $("#attachments").append(html);


    }



}




function removeFile(index, contactId) {


    var id = "file+" + index;

    let fileName = document.getElementById(id).getAttribute('data-fileName');

    $.ajax({
        type: "POST",
        url: "/mail/removeFile",
        data: { fileName: fileName, Id: contactId },
        success: function (result) {

            if (result) {

                notifySuccess('Attachment has been successfully deleted!');

                document.getElementById(id).style.display = 'none';

            } else {

                notifyError('Something went wrong while deleting file.');

            }

        },

        statusCode: {
            404: function () {
                notifyError('Source might have been changed, please try again later.');

            }

        }



    });


}
function hide(index) {

    var id = "file+" + index;

    document.getElementById(id).style.display = 'none';

}

jQuery(document).ready(function ($) {
    $(".click").click(function () {
        window.location = $(this).data("href");
    });
});


jQuery(document).ready(function ($) {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});



