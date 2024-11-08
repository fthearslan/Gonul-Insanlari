function firedelete(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert it.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/announcements/delete/" + id,
                success: function () {

                    Swal.fire({
                        title: "Deleted!",
                        text: "The announcement have been deleted.",
                        icon: "success"
                    }).then(function () {

                        window.location.reload();
                    });
                },
                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied',
                            text: 'You do not have an access to delete it.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });


                    }
                }

            });



        }


    });

}
$(".myBox").click(function () {
    window.location = $(this).find("a").attr("href");
    return false;
});

function attach(id) {


    $.ajax({



        type: 'post',
        url: '/announcements/attach/' + id,
        success: function (action) {

            notify(action+"!", "The announcement has been successfully "+action+".", "success");
            

        }



    })






}