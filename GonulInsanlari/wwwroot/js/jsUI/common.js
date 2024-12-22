
function subscribeNewsletter() {


    let MailAddress = $("#emailInput").val();



    if (MailAddress.length > 0) {

        $('#resultText').html('<p class="text-info">Mail adresinizi kontrol ediyoruz, lütfen bekleyiniz...</p>');


        $.ajax({


            type: 'Post',
            url: '/newsletter/subscribe',
            data: { MailAddress: MailAddress },
            success: function (result) {

                $('#resultText').html('<p class="text-success">Mail adresinize onaylama linki başarıyla gönderdildi. </p>');

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

        $('#resultText2').html('<p class="text-info">Mail adresinizi kontrol ediyoruz, lütfen bekleyiniz...</p>');


        $.ajax({


            type: 'Post',
            url: '/newsletter/subscribe',
            data: { MailAddress: MailAddress },
            success: function (result) {

                $('#resultText2').html('<p class="text-success">Mail adresinize onaylama linki başarıyla gönderdildi. </p>');

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


function quickSearch() {

    $("#results").empty();



    var input = $("#searchInput").val();

    let html = "";

    $.ajax({

        type: 'Post',
        url: '/article/search',
        data: { input: input },
        success: function (response) {


            for (var i = 0; i < response.length; i++) {









                html += '<li style="display:inline-block;"margin-bottom:5px;><a href="/article/' + response[i].slugTitle + '/' + response[i].id + '"> <svg style="margin-right:5px;" width="20px" height="20px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M4.4 3h15.2A3.4 3.4 0 0 1 23 6.4v11.2a3.4 3.4 0 0 1-3.4 3.4H4.4A3.4 3.4 0 0 1 1 17.6V6.4A3.4 3.4 0 0 1 4.4 3ZM7 9a1 1 0 0 1 1-1h8a1 1 0 1 1 0 2H8a1 1 0 0 1-1-1Zm1 2a1 1 0 1 0 0 2h8a1 1 0 1 0 0-2H8Zm-1 4a1 1 0 0 1 1-1h4a1 1 0 1 1 0 2H8a1 1 0 0 1-1-1Z" fill="#000000"/></svg>' + response[i].title + ' - '+response[i].date +' </a></li><br>'




            }


            $("#results").html(html);

        },
        statusCode: {
            404: function () {

                $("#results").html('<li style="display:inline-block;"><a href="#">Yazı bulunamadı.</a></li>');

            }
        }

    });




}



let timeout = null;
document.getElementById('searchInput').addEventListener('keyup', function (e) {


    // Clear existing timeout      
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {

        quickSearch();

    


    }, 500);

});



