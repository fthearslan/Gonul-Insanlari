function notifyError(message) {

    $.toast({
        heading: 'Error',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'error'

    });

}
function notifySuccess(message) {

    $.toast({
        heading: 'Success',
        text: message,
        showHideTransition: 'slide',
        position: 'top-right',
        icon: 'success'

    });

}
function pendOrDelete(id, action) {


    if (action == "deleted" || action == "pended") {

        Swal.fire({
            title: "Are you sure?",
            text: "Changes will be permanently.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes!"
        }).then((result) => {

            if (result.isConfirmed) {
                $.ajax({

                    type: 'Post',
                    url: '/subscriptions/pendOrDelete/' + id,
                    data: { action: action },
                    success: function (action) {
                        if (action == "activated") {

                            notifySuccess('The subscriber has been ' + action + ' successfully!')

                        }



                        setTimeout(function () {
                            window.location.reload();
                        }, 3000);


                    },
                    statusCode: {
                        404: function () {

                            notifyError('Something went wrong, please try again few minutes later.');
                        }

                    }


                })
            }


        });


    } else {
        $.ajax({

            type: 'Post',
            url: '/subscriptions/pendOrDelete/' + id,
            data: { action: action },
            success: function (action) {

                notifySuccess('The subscriber has been ' + action + ' successfully!')


                setTimeout(function () {
                    window.location.reload();
                }, 3000);


            },
            statusCode: {
                404: function () {

                    notifyError('Something went wrong, please try again few minutes later.');
                },
                400: function () {
                    notifyError('You can not activate this subscriber before he confirms his email address.');
                }
            }


        })
    }





}

function add() {

    var input = {
        Name: $("#name").val(),
        Surname: $("#surname").val(),
        MailAddress: $("#email").val()


    };


    $.ajax({

        type: 'POST',
        url: '/subscriptions/add',
        data: { model: input },
        success: function () {

            notifySuccess('Subscriber has been successfully added and a confirmation email has been sent to it.');
            setTimeout(function () {
                window.location.reload();
            }, 3000);



        },
        statusCode: {
            400: function (errors) {

                if (errors != null) {

                    let html = "";

                    for (var i = 0; i < errors.responseJSON.length; i++) {

                        html += '<li class="text-danger">' + errors.responseJSON[i] + '</li>';

                    }

                    $("#validation").empty();

                    $("#validation").append(html);
                }


            }
        }
    })




}

function clearInput() {

    document.getElementById('name').value = "";
    document.getElementById('surname').value = "";
    document.getElementById('email').value = "";

    $("#validation").empty();
}
