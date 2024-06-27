

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
                    text: 'The user has been successfully removed.',
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


function getPermissions(id) {


    let html = "";
    let checked = "";

    $.ajax({

        type: "GET",
        url: "/admin/roles/permissions/" + id,
        success: function (result) {

            for (let i = 0; i < result.length; i++) {

                if (result[i].exists) {

                    checked = "checked";
                }
                else {
                    checked = "";
                }

                html += '<tr >' +
                    '<td><i style="margin-right:3px;"  class="fa fa-id-badge" aria-hidden="true"></i> <span class="font-bold">' + result[i].permission + '</span></td>' +
                    '<td class="check-mail">' +
                    '<input type="checkbox"  class="i-checks" value="' + result[i].permission + '" ' + checked + ' >' +
                    '</td>' +
                    '</tr>';

            }


            $("#perms").html(html);

            let input = '<button type="button" id="btnEdit"  onclick="updatePermissions(' + id + ');" class="btn btn-primary"> Save </button>'

            $("#btnLocater").html(input);


        }

    })
}



function updatePermissions(id) {

    var perms = [];
    $('input[type="checkbox"]:checked').each(function () {
        perms.push($(this).val());
    });



    $.ajax({

        type: "POST",
        url: "/admin/roles/permissions/manage",
        data: { permissions: perms, roleId: id },
        success: function (result) {

            if (result) {

                $.toast({
                    heading: 'Success',
                    text: 'Permissions have been successfully added!',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

                $("#permissions").modal('hide');

            }
            else {

                $.toast({
                    heading: 'Error',
                    text: 'Something went wrong...',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });

            }

        }
    });







}


$('#btnEdit').on('click', function (e) {


    var object = {
        Name: $("#name").val(),
        RoleDescription: $("#roleDesc").val(),

    };



    $.ajax({

        type: "POST",
        url: "/admin/roles/add",
        data: { model: object },
        success: function (result) {

            if (result.success) {

                $.toast({
                    heading: 'Success',
                    text: 'The role has been successfully created!',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });
                setTimeout(function () {
                    window.location.reload();
                }, 3000);

                $("#newRole").modal('hide');


            }
            else {
                if (result.data != null) {

                    html = "";

                    for (let i = 0; i < result.data.length; i++) {

                        html += '<li class="text-danger">' + result.data[i] + '</li>';


                    }
                    $("#validation").empty();

                    $("#validation").append(html);
                }


            }

        }




    });

});


function deleteRole(id) {


    Swal.fire({
        title: "Are you sure?",
        text: "All the users in this role will be moved to role named Admin.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {


            $.ajax({
                type: "POST",
                url: "/admin/roles/delete/" + id,
                success: function (result) {

                    if (result) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "The role has been deleted.",
                            icon: "success"


                        }).then(function () {
                            window.location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "The role could not be deleted.",
                            text: "Something went wrong...",
                            icon: "error"


                        });

                    }


                }

            });




        }


    });





















}

function getUsers(id) {



    $.ajax({

        type: "GET",
        url: "/admin/roles/getUsers/" + id,
        success: function (result) {

            let html = "";

            $("#usersToAdd").empty();


            for (let i = 0; i < result.length; i++) {


                html += '<tr>' +
                    '<td><i class="fa fa-user"></td>' +
                    '<td>' + result[i].username + '</td>' +
                    '<td><input type="checkbox" class="i-checks" value="' + result[i].id + '" /></td>' +
                    '</tr>';

            }

            $("#usersToAdd").append(html);

            let input = '<button type="button" id="btnSave"  class="btn btn-primary" onclick="addUser('+id+');"> Save </button>';
            $("#btnLocater2").html(input);


        },
        statusCode: {
            404: function () {
                $.toast({
                    heading: 'Error',
                    text: 'There is no found.',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });
            }
        }

    });




}
function addUser(id) {

  

    var users = [];
    $('input[type="checkbox"]:checked').each(function () {
        users.push($(this).val());
    });



    if (users.length > 0) {



        let msg = "";

        if (users.length == 1) {

            msg = "User has been successfully added!";
        } else {
            msg = "Users have been successfully added!";

        }


        $.ajax({

            type: "POST",
            url: "/admin/roles/addUser/"+id,
            data: { Users: users },
            success: function (result) {

                if (result) {

                    $.toast({
                        heading: 'Success',
                        text: msg,
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    setTimeout(function () {
                        window.location.reload();
                    }, 3000);

                    $("#newUser").modal('hide');

                    $('table.user').ajax.reload(null, false);


                }
                else {

                    $.toast({
                        heading: 'Error',
                        text: 'Something went wrong...',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'error'

                    });

                }

            },
            statusCode: {
                404: function () {

                    $.toast({
                        heading: 'Error',
                        text: 'Role or users cannot be found.',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'error'

                    });

                }
            }

        });

    }
    else {

        $.toast({
            heading: 'Error',
            text: 'Please, select a user first.',
            showHideTransition: 'slide',
            position: 'top-right',
            icon: 'error'

        });

    }







}