
function subscribeNewsletter() {


    let MailAddress = $("#emailInput").val();



    if (MailAddress.length > 0) {

        $('#resultText').html('<p class="text-info">We are checking your email, please wait...</p>');


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

function subscribeNewsletterFooter() {


    let MailAddress = $("#emailInput2").val();


    if (MailAddress.length > 0) {

        $('#resultText2').html('<p class="text-info">We are checking your email, please wait...</p>');


        $.ajax({


            type: 'Post',
            url: '/newsletter/subscribe',
            data: { MailAddress: MailAddress },
            success: function (result) {

                $('#resultText2').html('<p class="text-success">Confirmation email has been successfully sent, please check your email.</p>');

            },
            statusCode: {
                400: function (errors) {

                    let html = "";

                    for (var i = 0; i < errors.responseJSON.length; i++) {

                        html += '<li class="text-danger">' + errors.responseJSON[i] + '</li>';

                    }

                    $('#resultText2').html('<ul>' + html + '</ul>');



                },
                404: function () {

                    $('#resultText2').html('error');


                }


            }



        })
    }


}