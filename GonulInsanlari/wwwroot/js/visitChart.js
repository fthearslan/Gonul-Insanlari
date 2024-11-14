$(function () {

   

    $.ajax({

        type: 'get',
        url: '/home/visits',
        success: function (response) {

            let data1 = [];

            data1 = response;


            var barData = {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [
                    {
                        label: "Data 1",
                        backgroundColor: 'rgba(220, 220, 220, 0.5)',
                        pointBorderColor: "#fff",
                        data: data1
                    }
                 

                ]
            };

            var barOptions = {
                responsive: true
            };


            var ctx2 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });






        }
        

    });




})