
function submit() {

    $("#validationErrors").html("");

    var input = {

        NameSurname: $("#name").val(),
        From: $("#from").val(),
        Subject: $("#subject").val(),
        Content: $("#content").val(),


    }


    $.ajax({



        type: 'post',
        url: '/contact-us/submit',
        data: { model: input },
        success: function () {
            $("#validations").append('<li class="text-success">Your message has been successfully sent.</li>');

        },
        statusCode: {
            400: function (errors) {


                for (var i = 0; i < errors.responseJSON.length; i++) {

                    $("#validations").append('<li class="text-danger">*' + errors.responseJSON[i] + '</li>');

                }


            }
        }

    })



}