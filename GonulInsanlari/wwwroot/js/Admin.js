function getUserInfo() {


    $.ajax({
        type: "GET",
        url: "/admin/u/edit",
        success: function (result) {

            $("#name").val(result.name);
            $("#username").val(result.userName);
            $("#surname").val(result.surname);
            $("#email").val(result.email);
            $("#age").val(result.age);
            $("#aboutme").val(result.aboutMe);
            $("#phonenumber").val(result.phoneNumber);
            $("#textbox1").val(result.imagePath);


        }
    });


};


$('#btnEdit').on('click', function (e) {
    let pInfo = "";
    let aboutMe = "";
    let html = "";
    var object = {

        Id: $("#userId").val(),
        UserName: $("#username").val(),
        Name: $("#name").val(),
        Surname: $("#surname").val(),
        Email: $("#email").val(),
        Age: $("#age").val(),
        PhoneNumber: $("#phonenumber").val(),
        AboutMe: $("#aboutme").val(),

    };



    $.ajax({

        type: "POST",
        url: "/admin/u/edit",
        data: { model: object },
        enctype: 'multipart/form-data',
        success: function (result) {

            if (result.success) {

                $.toast({
                    heading: 'Success',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

                $("#editUserInfo").modal('hide');

                $("#personalInfo").empty();
                $("#aboutMe").empty();

                pInfo = '<div class="row">' +
                    '<address style="margin-right:5px;margin-left:20px;">' +
                    '<strong>Full Name</strong><br>' +
                    '' + result.data.name + ' ' + result.data.surname + '<br>' +
                    '<br />' +
                    '<strong>Username</strong><br>' +
                    '' + result.data.userName + '<br>' +
                    '<br />' +
                    '<strong>Age</strong><br>' +
                    '' + result.data.age + '<br>' +
                    '<br />' +

                    '<strong>Phone Number</strong><br>' +
                    '' + result.data.phoneNumber + '<br>' +
                    '<br />' +

                    '<strong>Email</strong><br>' +
                    '<a href="mailto:#">' + result.data.email + '</a><br>' +
                    '<br />' +


                    '</address>' +
                    '<address style="margin-right:auto; margin-left:auto;">' +
                    '<strong>Social Media</strong><br>' +
                    '@fthearslan12<br>' +
                    '<br />' +
                    '<a href="#" style="color:black" data-toggle="modal" data-target="#editUserInfo" onclick="getUserInfo();"><i class="fa fa-pencil-square-o"></i> Edit</a>' +
                    '</address>' +
                    '</div>';

                aboutMe = '<h3>About ' + result.data.name + ' ' + result.data.surname + '</h3>' +
                    '<p>' +
                    result.data.aboutMe +
                    '</p>';



                // result.data dan donen degerler profile page de yerlerine yazilacak.


                $("#personalInfo").html(pInfo);
                $("#aboutMe").html(aboutMe);




            }
            else {
                if (result.errors != null) {


                    for (let i = 0; i < result.errors.length; i++) {

                        html += '<li class="text-danger">' + result.errors[i] + '</li>';


                    }
                    $("#validation").empty();

                    $("#validation").append(html);
                }


            }

        }




    });

});

jQuery(document).ready(function ($) {
    $(".logs").click(function () {
        window.location = $(this).data("href");
    });
});

function showImageName(input) {
    var file = $("#fileUpload").val().split('\\').pop();;
    document.getElementById("textbox1").value = file;
}

function getPofilePicture() {



    $.ajax({
        type: "GET",
        url: "/admin/u/edit/picture",
        success: function (result) {

            $("#textbox1").val(result);


        }
    });


}

$('#btnEditPicture').on('click', function (e) {

    let html = "";

    var formData = new FormData();
    var file = document.getElementById("fileUpload").files[0];

    formData.append("file", file);



    $.ajax({

        type: "POST",
        url: "/admin/u/edit/picture",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {

            if (result.success) {


                $.toast({
                    heading: 'Success',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });

                $("#editpicture").modal('hide');


                $("#textbox1").val(result.data);
                setTimeout(function () {
                    window.location.reload();
                }, 3000);




            }
            else {

                $.toast({
                    heading: 'Error',
                    text: result.responseMessage,
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });


            }

        }




    });



});

$('#btnPassword').on('click', function (e) {

    var formData = {

        CurrentPassword: $("#currentPassword").val(),
        NewPassword: $("#newPassword").val(),
        ConfirmPassword: $("#confirmPassword").val(),

    };
    let html = "";

    if (formData.NewPassword == formData.ConfirmPassword) {

        $.ajax({

            type: "POST",
            url: "/admin/u/changePassword",
            data: { model: formData },
            success: function (result) {

                if (result.success) {
                    $.toast({
                        heading: 'Success',
                        text: result.responseMessage,
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });
                    $("#changePassword").modal('hide');

                } else {

                    if (result.data != null) {


                        for (let i = 0; i < result.data.length; i++) {

                            html += '<li class="text-danger">' + result.data[i] + '</li>';


                        }
                        $("#pswrdVld").empty();

                        $("#pswrdVld").append(html);
                    }



                }

            }
        });

    } else {

        $.toast({
            heading: 'Error',
            text: 'Passwords do not match.',
            showHideTransition: 'slide',
            position: 'top-right',
            icon: 'error'

        });
    }






});

$('#chngPass').on('click', function (e) {

    $("#pswrdVld").empty();

    document.getElementById("newPassword").value = "";
    document.getElementById("currentPassword").value = "";
    document.getElementById("confirmPassword").value = "";





});


function getUserLogins(id) {

    let html = "";

    $.ajax({
        type: "GET",
        url: "/admin/u/getuserlogins/" + id,
        success: function (result) {

            for (let i = 0; i < result.length; i++) {

                let cls = "";

                if (result[i].type == "Login") {
                    cls = "text-navy"

                };

                if (result[i].type == "Logout") {
                    cls = "text-danger"

                };

                html += '<tr>' +
                    '<td>' + result[i].description + '</td>' +
                    '<td class="' + cls + '">' + result[i].type + '</td >' +
                    '</tr>';

                $("#userlog").html(html);


            }


        }



    });
};

let timeout = null;
document.getElementById('searchbar').addEventListener('keyup', function (e) {


    // Clear existing timeout      
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {

        var id = $("#userId").val();
        Search(id)

    }, 500);

});


function Search(id) {

    $("#userlog").empty();


    let html = "";

    var input = {
        Id: id,
        Search: document.getElementById('searchbar').value
    }

    $.ajax({
        type: "POST",
        url: "/admin/u/search",
        data: { model: input },
        success: function (result) {


            if (result == 0) {
                let msg = '<tr>' +
                    '<td>There is no login found.</td>' +
                    '<td></td>' +
                    '</tr>';

                $("#userlog").html(msg);

            }

            for (let i = 0; i < result.length; i++) {

                let cls = "";

                if (result[i].type == "Login") {
                    cls = "text-navy"

                };

                if (result[i].type == "Logout") {
                    cls = "text-danger"

                };





                html += '<tr>' +
                    '<td>' + result[i].description + '</td>' +
                    '<td class="' + cls + '">' + result[i].type + '</td >' +
                    '</tr>';




                $("#userlog").html(html);





            }




        }
    })

};


function enableOrDisable(id, text) {


    let msg = "";
    let icon = "";
    let btnText = "";

    if (text == "Disable") {
        msg = "Disabled user will be permanently deleted within 14 days.";
        icon = "warning";
        btnText = "Yes, disable it!";

    }
    if (text=="Enable") {
        msg = "This user will be enabled.";
        icon = "warning";
        btnText = "Yes, enable it!";

    }
    if (text == "Delete") {
        msg = "This user will be deleted permanently.";
        icon = "warning";
        btnText = "Yes, delete it!";

    }
    
    Swal.fire({
        title: "Are you sure?",
        text: msg,
        icon: icon,
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: btnText
    }).then((result) => {

        if (result.isConfirmed) {



            $.ajax({

                type: "POST",
                url: "/admin/u/users/delete/" + id,
                data: {action:text},
                success: function (response) {

                 

                        notifySuccess("User " + response + "d.");
                      
                 

                        setTimeout(function () {
                            window.location.reload();
                        }, 3000);

                    
                 

                },
                statusCode: {
                    403: function () {

                        $.toast({
                            heading: 'Error',
                            text: 'You do not have a permission.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });
                        
                    }
                    ,
                    400: function (response) {


                        notifyError(response.responseText);



                    }
                }

            });




        }


    });



}


//$('#btnAdd').on('click', function () {



//});

function register() {

    let html = "";

    var object = {

        UserName: $("#username").val(),
        Name: $("#name").val(),
        Surname: $("#surname").val(),
        Email: $("#email").val(),
        Age: $("#age").val(),
        PhoneNumber: $("#phoneNumber").val(),


    };

    $.ajax({

        type: "POST",
        url: "/admin/u/register/new",
        data: { model: object },
        enctype: 'multipart/form-data',
        success: function () {

            notifySuccess('User has been successfully created.');

                $("#add").modal('hide');

                setTimeout(function () {
                    window.location.reload();
                }, 3000);


        },
        statusCode: {

            400: function (errors) {
                if (errors != null) {

                    for (let i = 0; i < errors.responseJSON.length; i++) {

                        html += '<li class="text-danger">' + errors.responseJSON[i] + '</li>';


                    }

                    $("#validation").empty();

                    $("#validation").append(html);

                }



            },
            403: function () {

                notify('Access denied', 'You do not have an access to create a new user.','error')
               

            }

        }




    });
}

function clearInput() {

    document.getElementById('username').value = "";
    document.getElementById('name').value = "";
    document.getElementById('surname').value = "";
    document.getElementById('email').value = "";

    $("#validation").empty();


}