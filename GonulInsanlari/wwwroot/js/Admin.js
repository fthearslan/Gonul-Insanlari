

function getuserInfo() {


    $.ajax({
        type: "GET",
        url: "/admin/u/edit",
        success: function (result) {

            $("#name").val(result.name);
            $("#username").val(result.userName);
            $("#surname").val(result.surname);
            $("#email").val(result.email);
            $("#age").val(result.age);
            $("#about").val(result.aboutMe);
            $("#phonenumber").val(result.phoneNumber);


        }
    });


}