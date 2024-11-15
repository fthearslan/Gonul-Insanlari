$(function () {



    $.ajax({

        type: 'get',
        url: '/visitors/visits',
        success: function (response) {

            let months = [];
            let counts = [];

            months = response.months;
            counts = response.counts;


            var barData = {
                labels: months,
                datasets: [
                    {
                        label: "Visitor Count",
                        backgroundColor: 'rgba(14, 8, 204, 0.8)',
                        pointBorderColor: "#132a91",
                        data: counts
                    }


                ]
            };

            var barOptions = {
                responsive: true,
                scales: {
                    yAxes: [{
                        display: true,

                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: 10000,// minimum will be 0, unless there is a lower value.
                            // OR //
                            beginAtZero: true   // minimum value will be 0.
                        }

                    }],
                    xAxes: [{
                        // Change here
                        barPercentage: 0.2,
                        categoryPercentage: 0.4
                    }]
                }
            };


            var ctx2 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });






        }


    });

    $.ajax({

        type: 'get',
        url: '/visitors/visits/year',
        success: function (response) {


            if (response.length>0) {

                let html = "";

                for (var i = 0; i < response.length; i++) {


                    html += '<tr>' +
                        '<td> <i class=""> </i></td>' +
                        '<td>' + response[i].key + '</td>' +
                        '<td>' + response[i].value + '</td>' +
                        '</tr>';



                }

                $("#visitorTable").html(html);

            } else {


                let html = '<tr>' +
                    '<td> <i class=""> </i></td>' +
                    '<td>There is no record found for previous years.</td>' +
                    '</tr>';

                $("#visitorTable").html(html);


            }




        }



    });


})

function deleteRecords() {


    $.ajax({

        type: 'post',
        url: '/visitors/delete',
        success: function () {


            notifySuccess('All the records has been deleted successfully!');

        }


    })
}