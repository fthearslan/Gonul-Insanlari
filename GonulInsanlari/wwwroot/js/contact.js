



function Refresh() {

    $('#mails').empty();

    $.ajax({
        type: "POST",
        url: "/mail/refresh",
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

                var html = '<tr id="' + result[i].id + '" class="' + cls + '" style="cursor:pointer">' +
                    '<td class="check-mail">' +
                    '<input type="checkbox" class="i-checks" value="' + result[i].id + '">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-ontact">' + result[i].nameSurname + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-subject">' + result[i].subject + '-' + result[i].content + '<a href="/Admin/Contact/GetDetails/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + icon + ' ' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id + ')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';




                $("#mails").append(html);


            }

        }
    });
};

function RefreshSentBox() {

    $('#mails').empty();

    var status = $("#status").val();

    $.ajax({
        type: "POST",
        url: "/mail/refreshsentbox",
        data: { status: status },
        success: function (result) {


            for (let i = 0; i < result.length; i++) {



                var html = '<tr id="' + result[i].id + '" class="read click" style="cursor:pointer">' +
                    '<td class="check-mail">' +
                    '<input type="checkbox" class="i-checks" value="' + result[i].id + '">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-ontact">' + result[i].nameSurname + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-subject">' + result[i].subject + '-' + result[i].content + '<a href="/Admin/Contact/GetDetails/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id + ')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';




                $("#mails").append(html);


            }

        }
    });
};




function Send(id) {

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

                $.toast({
                    heading: 'Success',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

                for (let i = 0; i < allVals.length; i++) {

                    $("." + allVals[i]).hide();



                }

            }
            else {
                $.toast({
                    heading: 'Error',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });
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

                $.toast({
                    heading: 'Success',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

                for (let i = 0; i < allVals.length; i++) {

                    $("#" + allVals[i]).hide();



                }


            }
            else {
                $.toast({
                    heading: 'Error',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });
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


// New timeout variable
let timeout = null;
document.getElementById('searchbar').addEventListener('keyup', function (e) {


    // Clear existing timeout      
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {


        Search()

    }, 500);

});


function Search() {

    $('#mails').empty();

    var isSent = $("#isSent").val();
    var isdraft = $("#isdraft").val();
    var isTrash = $("#status").val();
    if (isTrash == "trash") {
        var IsToDelete = Boolean(true);

    }

    let value = document.getElementById('searchbar').value


    $.ajax({
        type: "POST",
        url: "/mail/search",
        data: { search: value, isSent: isSent, isdraft: isdraft, isTodelete: IsToDelete },
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
                if (result[i].isSent) {
                    cls = "read";
                    icon = "";
                } if (result[i].isDraft) {
                    cls = "read";
                    icon = "";
                }
                

                var html = '<tr id="' + result[i].id + '" class="' + cls + '" style="cursor:pointer">' +
                    '<td class="check-mail">' +
                    '<input type="checkbox" class="i-checks" value="' + result[i].id + '">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-ontact">' + result[i].nameSurname + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class="mail-subject">' + result[i].subject + '-' + result[i].content + '<a href="/Admin/Contact/GetDetails/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id + ')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + icon + ' ' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id + ')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';




                $("#mails").append(html);


            }

        }
    });





}




