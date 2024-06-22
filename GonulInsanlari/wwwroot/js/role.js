function remove(userId, roleName) {


    $.ajax({

        type: "POST",
        url: "/admin/roles/remove",
        data: { userId: userId, roleName: roleName },
        success: function (response) {


            if (response.success) {

                $("#" + userId).hide();

                $.toast({
                    heading: 'Success',
                    text: response.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

               

            }
            else {
                $.toast({
                    heading: 'Error',
                    text: response.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });



            }


        }
    })


}