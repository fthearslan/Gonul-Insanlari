
function editPrivacy() {

    var input = {

        Id: $('#Id').val(),
        Title: $('#title').val(),
        Content: $('#content').val()
    };

    $.ajax({

        type: 'post',
        url: '/privacy/edit',
        data: { model: input },
        success: function (action) {

            notifySuccess('Privacy statement has been successfully ' + action);



            setTimeout(function () {
                window.location.reload();
            }, 3000);


        },

        statusCode: {
            400: function (errors) {

                let html = "";

                for (var i = 0; i < errors.responseJSON.length; i++) {

                    html += '<li class="text-danger">' + errors.responseJSON[i] + '</li>';

                }

                $("#validation").empty();

                $("#validation").append(html);


            }

        }
    })



}

function clearRoleInput() {

    document.getElementById('title').value = "";
    document.getElementById('content').value = "";


    $("#validation").empty();


}

function getPrivacy(id) {

    $.ajax({
        type: 'post',
        url: '/privacy/getPrivacy',
        data: { id: id },
        success: function (privacy) {

            if (privacy != null) {

                document.getElementById('Id').value = privacy.Id;
                document.getElementById('title').value = privacy.title;
                $('#content').summernote('code', privacy.content);

            }



        }
        
    })



}