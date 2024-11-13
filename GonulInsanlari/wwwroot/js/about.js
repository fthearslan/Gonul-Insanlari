
function editAbout() {

    var input = {

        Id: $('#Id').val(),
        Title: $('#title').val(),
        Details: $('#details').val()
    };

    $.ajax({

        type:'post',
        url: '/about/edit',
        data: {model :input},
        success: function (action) {

            notifySuccess('About details has been successfully '+action);

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
    document.getElementById('details').value = "";


    $("#validation").empty();


}