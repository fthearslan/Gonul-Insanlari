function firedisable(id,type) {

    Swal.fire({
        title: "Are you sure?",
        text: "This will make article invisible to the users.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, disable it!"
    }).then((result) => {

        if (result.isConfirmed) {


            $.ajax({
                type: "POST",
                url: "/categories/delete/" + id,
                success: function () {
                    Swal.fire({
                        title: "Disabled!",
                        text: "The category has been successfully  disabled.",
                        icon: "success"


                    }).then(function () {
                        window.location.reload();
                    });

                },
                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied',
                            text: 'You do not have an access to '+type,
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

function firedelete(id,type) {

    Swal.fire({
        title: "Are you sure?",
        text: "This will delete the all the articles as well.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/categories/delete/" + id,
                success: function () {
                    Swal.fire({
                        title: "Deleted!",
                        text: "The category has been successfully deleted.",
                        icon: "success"


                    }).then(function () {
                        window.location.reload();
                    });

                },
                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied',
                            text: 'You do not have an access to ' + type,
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