



function Refresh() {

    $('#mails').empty();

    $.ajax({
        type: "POST",
        url: "/Admin/Contact/Refresh",
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
                    '<input type="checkbox" class="i-checks" value="'+ result[i].id +'">' +
                    ' </td>' +
                    '<td onclick="Send(' + result[i].id +')" class="mail-ontact">' + result[i].nameSurname + '<a href=""></a></td >' +
                    '<td onclick="Send(' + result[i].id +')" class="mail-subject">' + result[i].subject + '-' + result[i].content + '<a href="/Admin/Contact/GetDetails/' + result[i].id + '"></a></td >' +
                    '<td onclick="Send(' + result[i].id +')" class=""> </td>' +

                    '<td onclick="Send(' + result[i].id + ')" class="" > <i class="' + icon + ' ' + result[i].id + '" > </i></td >' +

                    '<td onclick="Send(' + result[i].id +')" class="text-right mail-date">' + result[i].createdDate + '</td>' +
                    '</tr>';




                $("#mails").append(html);


            }

        }
    });
};



function Send(id) {

    window.location = "/Admin/Contact/GetDetails/" + id;

};

function markAsRead() {

    var allVals = [];
    $('input[type="checkbox"]:checked').each(function () {
        allVals.push($(this).val());
    });


    $.ajax({
        type: "POST",
        data: { ids: allVals },
        url: "/Admin/Contact/MarkAsRead",
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
        url: "/Admin/Contact/Delete",
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


jQuery(document).ready(function ($) {
    $(".click").click(function () {
        window.location = $(this).data("href");
    });
});


$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});