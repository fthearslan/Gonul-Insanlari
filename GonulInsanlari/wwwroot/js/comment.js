jQuery(document).ready(function ($) {
    $(".click").click(function () {
        window.location = $(this).data("href");
    });
});

$(".myBox").click(function () {
    window.location = $(this).find("a").attr("href");
    return false;
});


function fireDelete(id) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert it..",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/comments/delete/" + id,



            });

            Swal.fire({
                title: "Disabled!",
                text: "The comment has been deleted.",
                icon: "success"


            }).then(function () {
                window.location.reload();
            });
        }


    });

}