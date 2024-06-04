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
