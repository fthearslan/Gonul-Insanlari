function pager(currentPageNumber) {
    $.ajax({
        type: "POST",
        url: "/category/all",
        data: {pageNumber:currentPageNumber + 1},
        success: function (result) {

            let html = "";

            for (var i = 0; i < result.length; i++) {


                html += '<img src="~/Images/' + result[i].imagePath + '" alt="" >' +
                '<p> ' + result[i].name +'</p>'; 


            }


            $("#catList").append(html);


        }
        

    });
}
