


function getComments(articleId,modelCount) {



    $.ajax({



        type: 'post',
        url: '/comment/get/' + articleId,
        data: { skipCount: modelCount },
        success: function (comments) {

            let html = "";


            for (var i = 0; i < comments.length; i++) {

                if (comments[i].replies.length > 0) {

                    var replies = comments[i].replies;

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

                        '<ul style="border:none" class="comment-replay">';

                    for (var i = 0; i < replies.length; i++) {


                        html += '<li>' +

                            '<div class="single-comment-area">' +
                            '<div class="author-img">' +
                            '<img src="/Images/profile.jpg" alt="">' +
                            '</div>' +
                            '<div class="comment-content">' +
                            '<div class="author-name-deg">' +
                            '<h6>' + replies[i].nameSurname + '</h6>' +
                            '<span>' + replies[i].displayedDateTime + '</span>' +
                            '</div>' +
                            '<p>' + replies[i].content + '</p>' +
                            '</div>' +
                            '</div>' +
                            '</li>';


                    }

                    html += '</ul>' +
                        '</li>';



                } else {

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


            $("#validationErrors").html('<p class="text-success">Yorumunuz başarıyla yapıldı.</p>');

        },
        statusCode: {
            400: function (errors) {

                for (var i = 0; i < errors.responseJSON.length; i++) {
                    $("#validationErrors").append('<li class="text-danger">' + errors.responseJSON[i] + '</li>');

                }



            }
        },


        error: function () {

            $("#validationErrors").append('<li class="text-danger">Yorumunuz işlenirken bir hata meydana geldi, lütfen bir kaç dakika sonra tekrar deneyiniz.</li>');

        }

    })


}