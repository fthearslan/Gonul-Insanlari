

function getComments(articleId,count) {

    

    $.ajax({



        type: 'post',
        url: '/comment/get/' + articleId,
        data: { skipCount: count },
        success: function (result) {    

            let html = "";


            for (var i = 0; i <result.length; i++) {



               html+='<li>'+
                    '<div class="single-comment-area">'+
                '<div class="author-img">'+
                     '<img src="~/Images/profile.jpg" alt="">'+
                        '</div>'+
                        '<div class="comment-content">'+
                            '<div class="author-name-deg">'+
                                '<h6>@comment.NameSurname</h6>'+
                                '<span>@comment.Created.ToShortDateString()</span>'+
                            '</div>'+
                            '<p>@comment.Content</p>'+
                        '</div>'+
                   '</div>' +
                    '</li>';


            }

            $("#comments").append(html);


        }

    })






}