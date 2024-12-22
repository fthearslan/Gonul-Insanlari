
function editAbout() {

    var input = {

        Id: $('#Id').val(),
        Title: $('#title').val(),
        Details: $('#details').val()
    };

    $.ajax({

        type: 'post',
        url: '/about/edit',
        data: { model: input },
        success: function (action) {

            notifySuccess('About details has been successfully ' + action);



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
    document.getElementById('details').value = "";


    $("#validation").empty();


}

function getAbout(id) {

    $.ajax({
        type: 'post',
        url: '/about/getAbout',
        data: { id: id },
        success: function (about) {
            if (about != null) {
                document.getElementById('Id').value = about.id;

                document.getElementById('title').value = about.title;
                $('#details').summernote('code', about.details);

            }


        }

    })



}