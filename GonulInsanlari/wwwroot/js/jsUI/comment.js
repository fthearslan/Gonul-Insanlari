const { error } = require("jquery");


function getComments(articleId, count) {



    $.ajax({



        type: 'post',
        url: '/comment/get/' + articleId,
        data: { skipCount: count },
        success: function (comments) {

            let html = "";


            for (var i = 0; i < comments.length; i++) {



                html += '<li>' +
                    '<div class="single-comment-area">' +
                    '<div class="author-img">' +
                    '<img src="/Images/profile.jpg" alt="">' +
                    '</div>' +
                    '<div class="comment-content">' +
                    '<div class="author-name-deg">' +
                    '<h6>' + comments[i].nameSurname + '</h6>' +
                    '<span>' + comments[i].displayedDateTime + '</span>' +
                    '</div>' +
                    '<p>' + comments[i].content + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</li>';


            }

            $("#comments").append(html);


        }

    })






}

function submit(articleId) {

    $("#validationErrors").html("");

    var formInput = {

        NameSurname: $("#nameSurname").val(),
        Email: $("#email").val(),
        Content: $("#content").val(),
        ArticleID: articleId

    }


    $.ajax({

        type: "Post",
        url: "/comment/submit",
        data: { input: formInput },
        success: function (result) {


            $("#validationErrors").html('<p class="text-success">Your comment has been successfully submitted.</p>');

        },
        statusCode: {
            400: function (errors) {

                for (var i = 0; i < errors.responseJSON.length; i++) {
                    $("#validationErrors").append('<li class="text-danger">' + errors.responseJSON[i] + '</li>');

                }



            }
        },


        error: function () {

            $("#validationErrors").append('<li class="text-danger">Something went wrong while proccessing your comment, please try again a few minute later.</li>');

        }

    })


}