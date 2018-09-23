document.addEventListener('DOMContentLoaded', function () {
    
    var values = [];
    var labels = [];

    

    function GetDataSet() {
        debugger
        $.ajax({
            type: "GET",
            url: "/Admin/GetDataSet",
            data: {},
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                debugger;
                values.push(result.Datasets.Quantity);
                labels.push(result.Datasets.DimensionOne);
            },
            error: function (response) {
                debugger;
                alert('eror');
            }
        });

        $(document).ready(function () {
            debugger
            GetDataSet();
        });


    

    

    var chart = new Chart(document.getElementById("chart"),
        {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        data: values,
                        backgroundColor: 'rgba(255, 99, 132, 0.1)',
                        borderColor: 'rgb(255, 99, 132)',
                        borderWidth: 2,
                        lineTension: 0.25,
                        pointRadius: 0
                    }
                ]
            },
            options: {
                responsive: false,
                animation: {
                    duration: speed * 1.5,
                    easing: 'linear'
                },
                legend: false,
                scales: {
                    xAxes: [
                        {
                            display: false
                        }
                    ],
                    yAxes: [
                        {
                            ticks: {
                                max: 1,
                                min: -1
                            }
                        }
                    ]
                }
            }
        });

        $('#availableMonthsDropdown').change(function () {
            var month = $(this).val();
            $("#availableMonthsForm").ajaxSubmit({
                url: '/Admin/GetDataSet',
                type: 'post',
                contentType: "application/json;charset=utf-8",
                data: {
                    userToShow: $("#userOptionDropdown").val(),
                    monthToShow: month
                },
                success: function (data) {
                    debugger
                    values.push(data.DataSets.Quantity);
                    labels.push(data.Datasets.DimensionOne);
                    chart.update();
 
                }
            });

        });

    //connection.on('Broadcast',
    //    function (sender, message) {
    //        values.push(message.value);
    //        values.shift();

    //        chart.update();
    //    });

    //connection.start();
    };
});











//$(document).ready(function () {
//$(function () {
//    var chartName = "barChart";
//    var ctx = document.getElementById(chartName).getContext('2d');
//    var data = {
//        labels: @Html.Raw(XLabels),
//        datasets: [{
//            label: "Differans",
//            backgroundColor: [
//                'rgba(255, 99, 132, 0.2)',
//                'rgba(54, 162, 235, 0.2)',
//                'rgba(255, 206, 86, 0.2)',
//                'rgba(75, 192, 192, 0.2)',
//                'rgba(153, 102, 255, 0.2)',
//                'rgba(255, 159, 64, 0.2)',
//                'rgba(255, 0, 0)',
//                'rgba(0, 255, 0)',
//                'rgba(0, 0, 255)',
//                'rgba(192, 192, 192)',
//                'rgba(255, 255, 0)',
//                'rgba(255, 0, 255)'
//            ],
//            borderColor: [
//                'rgba(255,99,132,1)',
//                'rgba(54, 162, 235, 1)',
//                'rgba(255, 206, 86, 1)',
//                'rgba(75, 192, 192, 1)',
//                'rgba(153, 102, 255, 1)',
//                'rgba(255, 159, 64, 1)',
//                'rgba(255, 0, 0)',
//                'rgba(0, 255, 0)',
//                'rgba(0, 0, 255)',
//                'rgba(192, 192, 192)',
//                'rgba(255, 255, 0)',
//                'rgba(255, 0, 255)'
//            ],
//            borderWidth: 1,
//            /**/
//        data: @Html.Raw(YValues),
//            /**/
//        }]
//    };

//    var options = {
//        maintainAspectRatio: false,
//        scales: {
//            yAxes: [{
//                ticks: {
//                    min: 0,
//                    beginAtZero: true
//                },
//                gridLines: {
//                    display: true,
//                    color: "rgba(255,99,164,0.2)"
//                }
//            }],
//            xAxes: [{
//                ticks: {
//                    min: 0,
//                    beginAtZero: true
//                },
//                gridLines: {
//                    display: false
//                }
//            }]
//        }
//    };

//    var myChart = new Chart(ctx, {
//        options: options,
//        data: data,
//        type: 'bar'

//    });
//});
//}