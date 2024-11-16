function submit() {


    var input = {


        Name: $("#name").val(),
        Surname: $("#surname").val(),
        Email: $("#email").val()


    } 



    $.ajax({


        type: 'Post',
        url: '/email/confirm',
        data: { model: input },
        success: function (result) {

           window.location.href = "/email/EmailConfirmed";

        },
        statusCode: {
            400: function (errors) {


                for (var i = 0; i < errors.responseJSON.length; i++) {

                    $("#validations").append('<li class="text-danger">*' + errors.responseJSON[i] + '</li>');

                }


            }
        }

    });

}