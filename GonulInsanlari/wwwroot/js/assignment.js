
function TaskonChange(id, assignmentId) {

    var text = $('#' + id + ' option:selected').text();

    var data = {
        Progress: text,
        SubTaskId: id,
        TaskId: assignmentId
    };

    $.ajax({
        type: 'POST',
        url: '/assignments/changeProgress',
        data: { obj: data },
        success: function (result) {
            $.toast({
                heading: 'Success',
                text: 'Progress has been successfully changed.',
                showHideTransition: 'slide',
                position: 'top-right',
                icon: 'success'
            });
        },

        statusCode: {

            403: function () {
                $.toast({
                    heading: 'Access denied',
                    text: 'You do not have an access to change the progress.',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'
                });

            },

            404: function () {
                $.toast({
                    heading: 'Not found',
                    text: 'Source might have been changed or deleted.',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'
                });
            }
        }


    })

};



function deleteUser(id, assignmentId) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            var AssignmentId = assignmentId;
            var userID = id;

            $.ajax({
                type: "POST",
                data: { assignmentId: AssignmentId, userId: userID },
                url: "/assignments/remove/user",
                success: function () {

                    $.toast({
                        heading: 'Success',
                        text: 'Users has been successfully removed!',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    $("#" + userID).hide();

                },
                statusCode: {
                    403: function () {

                        $.toast({
                            heading: 'Access Denied',
                            text: 'You do not have an access to delete user.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });

                    },

                    404: function () {

                        $.toast({
                            heading: 'Not Found',
                            text: 'Source of this task might have been changed.',
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

function addSubtask(assignmentId) {

    var subtask = {
        TaskId: assignmentId,
        SubTaskDescription: $('#description').val(),
    };

    if ($("#description").val().trim().length < 1) {
        $.toast({
            heading: 'Error',
            text: 'Field is required!',
            showHideTransition: 'slide',
            position: 'top-right',
            icon: 'error'

        })
        return;
    }

    if ($("#description").val().length < 15) {
        $.toast({
            heading: 'Error',
            text: 'Too short!',
            showHideTransition: 'slide',
            position: 'top-right',
            icon: 'error'

        })
        return;
    }




    $.ajax({
        type: "POST",
        data: { model: subtask },
        url: "/assignments/add/subtask",
        success: function (result) {
            $.toast({
                heading: 'Success',
                text: 'Subtask has been added succesfully.',
                showHideTransition: 'slide',
                position: 'top-right',
                icon: 'success'

            });



            $("#addSubtask").hide();

            setTimeout(function () {
                window.location.reload();
            }, 3000);
        },

        statusCode: {
            403: function () {
                $.toast({
                    heading: 'Access denied',
                    text: 'You do not have an access to add subtask.',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'error'

                });


            }
        }
    });


}


function deleteSubtask(id, assignmentId) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            var AssignmentId = assignmentId;
            var subtaskID = id;
            $.ajax({
                type: "POST",
                data: { assignmentId: AssignmentId, subtaskId: subtaskID },
                url: "/assignments/remove/subtask/",
                success: function () {
                    $.toast({
                        heading: 'Success',
                        text: 'Subtask has been  succesfully deleted!',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    $("#tr" + id).hide();

                },

                statusCode: {
                    403: function () {
                        $.toast({
                            heading: 'Access denied',
                            text: 'You do not have an access to delete this subtask.',
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


function deleteAssignment(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/assignments/delete/' + id,
                success: function (response) {

                    $.toast({
                        heading: 'Success',
                        text: 'The assignment has been successfully deleted.',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'success'

                    });

                    $("#" + id).hide();

                },

                statusCode: {

                    403: function () {

                        $.toast({
                            heading: 'Access denied',
                            text: 'You do not have an access to delete this task.',
                            showHideTransition: 'slide',
                            position: 'top-right',
                            icon: 'error'

                        });
                    },
                    404: function () {

                        $.toast({
                            heading: 'Not Found',
                            text: 'Task could not be found.',
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


$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
});

function Reload() {
    window.location.reload();
}

function AddUser(assignmentId) {

    var users = [];
    $('input[type="checkbox"]:checked').each(function () {
        users.push($(this).val());
    });

    if (users.length > 0) {

        var TaskId = assignmentId;

        $.ajax({
            type: "POST",
            data: { users: users, assignmentId: assignmentId },
            url: "/assignments/add/user",
            success: function (result) {
                $.toast({
                    heading: 'Success',
                    text: 'Users has been successfully added.',
                    showHideTransition: 'slide',
                    position: 'top-right',
                    icon: 'success'

                });


                for (let i = 0; i < users.length; i++) {

                    $("#" + users[i]).hide();

                }



                setTimeout(function () {
                    window.location.reload();
                }, 3000);

            },
            statusCode: {
                403: function () {

                    $.toast({
                        heading: 'Access Denied',
                        text: 'You do not have an access to add user.',
                        showHideTransition: 'slide',
                        position: 'top-right',
                        icon: 'error'

                    });

                },

                404: function () {
                    $.toast({
                        heading: 'Not found',
                        text: 'Source might have been changed.',
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
            text: 'Please, select a user first.',
            showHideTransition: 'slide',
            position: 'top-right',
            icon: 'error'

        })

    }


}


function UploadFiles(inputId, taskId) {


    var input = document.getElementById(inputId);

    var files = input.files;


    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("Attachments", files[i]);
    }

    formData.append("TaskId", taskId);
    $.ajax(
        {
            url: "/assignments/add/file",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (result) {
                if (result.success) {

                    notifySuccess(result.responseMessage);

                    var obj = result.data;


                    for (let i = 0; i < obj.length; i++) {

                        var html = '<div class="file-box">' +

                            '<div class="file">' +


                            '<span class="corner"></span>' +

                            '<div class="icon">' +
                            '<i class="fa fa-file"></i>' +
                            '</div>' +
                            '<a href="/assignments/downloadFile/' + obj[i].path + '" >' +

                            '<div class="file-name">'
                            + obj[i].path +

                            '<br />' +
                            '<small> Added: ' + obj[i].createdDate + '</small>' +
                            '</div>' +
                            '</a>' +
                            '</div>' +
                            '</div>';

                        $("#mydiv1").append(html);


                    }

                }
                else {

                    notifyError(result.responseMessage);


                }

            }
        }
    );
}

//function downloadFile(fileName) {

//    let fileNameTrimmed = fileName.replace(" ", "");

//    $.ajax(
//        {
//            url: "/assignments/downloadFile/" + fileNameTrimmed,
//            type: "POST",
//            success: function (result) {
               
//            },
//            statusCode: {
//                404: function () {
//                    notifyError('Source might have been changed.');
//                }
//            }



//        });

//        }


