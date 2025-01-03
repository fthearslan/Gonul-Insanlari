﻿jQuery(document).ready(function ($) {
    $(".click").click(function () {
        window.location = $(this).data("href");
    });
});

$(".myBox").click(function () {
    window.location = $(this).find("a").attr("href");
    return false;
});


function deleteComment(id) {

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
                success: function () {

                    $.toast({
                        heading: 'Success',
                        text: 'Comment has been successfully deleted.',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    $("#" + id).hide();

                },
                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied!',
                            text: 'You do not have an access to delete this comment.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });

                    },
                    404: function () {
                        $.toast({
                            heading: 'Not found',
                            text: 'Source might have been changed.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });
                    }

                }


            });


        }


    });

}


function deleteReply(replyId,commentId) {

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
                url: "/comments/deleteReply",
                data: { commentId: commentId, replyId: replyId },
                success: function () {

                    $.toast({
                        heading: 'Success',
                        text: 'Comment has been successfully deleted.',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    $("#" + id).hide();

                },
                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied!',
                            text: 'You do not have an access to delete this comment.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });

                    },
                    404: function () {
                        $.toast({
                            heading: 'Not found',
                            text: 'Source might have been changed.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });
                    }

                }


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

        var CommentProgress = $("#progress").val();

        if (CommentProgress.length > 0) {
            SearchComments()

        } else {
            searchCommentsByArticleDetails();
        }


    }, 500);

});



function SearchComments() {

    $('#panel').empty();




    var searchModel = {

        Search: document.getElementById('searchbar').value,
        Progress: $("#progress").val()

    };


    $.ajax({
        type: "POST",
        url: "/comments/search",
        data: { model: searchModel },
        success: function (result) {

            for (let i = 0; i < result.length; i++) {

                var labelstatus = "";

                var actionValueFirst = "";
                var actionValueSec = "";
                var btnFirstText = "";
                var btnSecText = "";

                var btnFirstcls = "";
                var btnSeccls = "";

                if (result[i].status == true) {
                    labelstatus = "text-navy"
                } else {
                    labelstatus = "text-danger"
                }

                switch (result[i].progress) {
                    case "Pending":
                        actionValueFirst = "approve";
                        btnFirstText = " Approve";
                        btnSecText = " Reject";
                        btnFirstcls = "fa fa-thumbs-up";
                        btnSeccls = "fa fa-thumbs-down";
                        actionValueSec = "reject";
                        break;
                    case "Approved":
                        actionValueFirst = "";
                        actionValueSec = "reject";
                        btnFirstText = " Unknown";
                        btnSecText = " Reject";
                        btnFirstcls = "fa fa-thumbs-up";
                        btnSeccls = "fa fa-thumbs-down";
                        break;
                    case "Rejected":
                        actionValueFirst = "approve";
                        actionValueSec = "disable";
                        btnFirstText = " Approve";
                        btnSecText = " Disable";
                        btnFirstcls = "fa fa-thumbs-up";
                        btnSeccls = "fa fa-trash";
                        break;
                    case "Disabled":
                        actionValueFirst = "save";
                        actionValueSec = "delete";
                        btnFirstText = " Save";
                        btnSecText = " Delete";
                        btnFirstcls = "fa fa-repeat";
                        btnSeccls = "fa fa-trash";
                        break;
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
                    '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="' + actionValueFirst + '" class="btn btn-primary btn-xs"><i class="' + btnFirstcls + '"></i>' + btnFirstText + '</button>' +
                    '<a href="/articles/details/' + result[i].articleID + '" class="btn btn-warning btn-xs"><i class="fa fa-file"></i> Go to Article</a>' +
                    '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="' + actionValueSec + '" class="btn btn-danger btn-xs"><i class="' + btnSeccls + '"></i>' + btnSecText + '</button>' +
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

function searchCommentsByArticleDetails() {

    let html = "";

    var searchModel = {

        Search: document.getElementById('searchbar').value,
        ArticleTitle: document.getElementById('articleTitle').value

    };


    $.ajax({
        type: "POST",
        url: "/comments/search",
        data: { model: searchModel },
        success: function (result) {

            for (let i = 0; i < result.length; i++) {



                switch (result[i].progress) {

                    case "Pending":

                        html += '<div id="' + result[i].id + '" class="social-feed-box">' +
                            '<div class="float-right social-action dropdown">' +
                            '<ul>' +
                            '<li style="list-style:none"><span class="text-warning"> Pending </span></li>' +
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
                            '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="reject" class="btn btn-danger btn-xs"><i class="fa fa-thumbs-down"></i> Reject</button>' +
                            '</div>' +
                            '</div>' +
                            '</div>';

                        break;

                    case "Approved":

                        html += '<div id="' + result[i].id + '" class="social-feed-box">' +
                            '<div class="float-right social-action dropdown">' +
                            '<ul>' +
                            '<li style="list-style:none"><span class="text-navy"> Approved</span></li>' +
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
                            '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="reject" class="btn btn-danger btn-xs"><i class="fa fa-thumbs-down"></i> Reject</button>' +
                            '</div>' +
                            '</div>' +
                            '</div>';

                        break;

                    case "Rejected":

                        html += '<div id="' + result[i].id + '" class="social-feed-box">' +
                            '<div class="float-right social-action dropdown">' +
                            '<ul>' +
                            '<li style="list-style:none"><span class="text-danger"> Rejected</span></li>' +
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
                            '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="disable" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i> Disable </button>' +
                            '</div>' +
                            '</div>' +
                            '</div>';




                        break;

                    case "Disabled":

                        html += '<div id="' + result[i].id + '" class="social-feed-box">' +
                            '<div class="float-right social-action dropdown">' +
                            '<ul>' +
                            '<li style="list-style:none"><span class="text-danger"> Disabled </span></li>' +
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
                            '<button onclick="approveOrReject(' + result[i].id + ',this.value)"  value="save" class="btn btn-success btn-xs"><i class="fa fa-repeat"></i> Save </button>' +
                            '</div>' +
                            '</div>' +
                            '</div>';


                        break;
                }




            }

            $("#panel").html(html);

        }
    });
}

function reply(commentId) {

    let inputId = '#reply' + commentId;

    var replyInput = {
        Content: $(inputId + " " + inputId).val(),
        CommentId: commentId
    

    }

    $.ajax({


        type: 'post',
        url: '/comments/reply',
        data: { input: replyInput },
        success: function () {


            notifySuccess("Your reply has been succesfully submitted.");




        },
        statusCode: {
            400: function () {

                notifyError("Source might have been changed, please try again a few minutes later.");

            }
        }

    })



}