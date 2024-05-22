jQuery(document).ready(function ($) {
    $(".click").click(function () {
        window.location = $(this).data("href");
    });
});

$(".myBox").click(function () {
    window.location = $(this).find("a").attr("href");
    return false;
});


function fireDelete(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert it..",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/comments/delete/" + id,



            });

            Swal.fire({
                title: "Disabled!",
                text: "The comment has been deleted.",
                icon: "success"


            }).then(function () {
                window.location.reload();
            });
        }


    });

}

let timeout = null;
document.getElementById('searchbar').addEventListener('keyup', function (e) {


    // Clear existing timeout      
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {


        SearchComments()

    }, 500);

});



function SearchComments() {

    $('#panel').empty();

    var CommentProgress = $("#progress").val();
    var Status = $("#Status").val();


    let value = document.getElementById('searchbar').value


    $.ajax({
        type: "POST",
        url: "/comments/search",
        data: { search: value, progress: CommentProgress, status: Status },
        success: function (result) {

            for (let i = 0; i < result.length; i++) {

                var labelstatus = "";
                

              
                if (result[i].status == true) {
                    labelstatus = "text-navy"
                } else {
                    labelstatus = "text-danger"
                }


                var html = '<div id="' + result[i].id + '" class="social-feed-box">' +
                    '<div class="float-right social-action dropdown">' +
                    '<ul>' +
                    '<li style="list-style:none">Status:<span class="' + labelstatus + '">' + result[i].status + '</span></li>' +
                    '</ul>' +
                    '</div>' +

                    '<div class="social-avatar">' +
                    '<a href="#" class="float-left">' +
                    '<img alt="image" src="/Images/profile.jpg">' +
                    '</a>' +
                    '<div class="media-body">' +
                    '<a><strong>' + result[i].nameSurname + '</strong><a>' +
                    '<small class="text-muted">' + result[i].created + '</small>' +
                    '</div>' +
                    '</div>' +
                    '<div class="social-body">' +
                    '<p>' + result[i].content + '</p>' +
                    '<div class="btn-group">' +
                    '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="approve" class="btn btn-primary btn-xs"><i class="fa fa-thumbs-up"></i> Approve</button>' +
                    '<a href="/articles/details/' + result[i].articleID + '" class="btn btn-warning btn-xs"><i class="fa fa-file"></i> Go to Article</a>' +
                    '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="reject" class="btn btn-danger btn-xs"><i class="fa fa-thumbs-down"></i> Reject </button>' +
                    '</div>' +
                    '</div>' +
                    '</div>';



                $("#panel").append(html);


            }

        }
    });





}

function approveOrReject(id, value) {

    $.ajax({
        type: "POST",
        data: { commentId: id, action: value },
        url: "/comments/update",
        success: function (result) {
            if (result.success) {
                $.toast({
                    heading: 'Success',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                })

                $("#" + id).hide();

            } else {
                $.toast({
                    heading: 'Error',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                })
            }

        }
    });


}


$(document).ready(function () {
    $("#dataTable").dataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.13.7/i18n/en-GB.json"
        },
        "searching": true,
        "ordering": true,
        "paging": true,
        "pagingType": "full_numbers",
        "pageLength": 10,
        "responsive": true,
        "columnDefs": [{
            "targets": 3,
            "orderable": false
        }]
    });
});