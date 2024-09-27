
function subscribeNewsletter() {


    let MailAddress = $("#emailInput").val();

    if (MailAddress.length > 0) {

        $.ajax({


            type: 'Post',
            url: '/newsletter/subscribe',
            data: { MailAddress: MailAddress },
            success: function (result) {

                $('#resultText').html('<p class="text-success">Confirmation email has been successfully sent, please check your email.</p>');

            },
            statusCode: {
                400: function (errors) {

                    let html = "";

                    for (var i = 0; i < errors.responseJSON.length; i++) {

                        html += '<li class="text-danger">' + errors.responseJSON[i] + '</li>';

                    }

                $('#resultText').html('<ul>' + html + '</ul>');


                },
                404: function () {

                    $('#resultText').html('error');

                }


            }



        })
    }


}