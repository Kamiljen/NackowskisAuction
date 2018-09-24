$(document).ready(function () {
var labels = [];
var yValues = [];
var speed = 250;
var charts = [];
var value = 0;
var samples = 100;
var ctx = document.getElementById("barChart").getContext('2d');

var data = {
    labels:labels,
    datasets: [{
        label: "Auktion differans",
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)',
            'rgba(255, 0, 0)',
            'rgba(0, 255, 0)',
            'rgba(0, 0, 255)',
            'rgba(192, 192, 192)',
            'rgba(255, 255, 0)',
            'rgba(255, 0, 255)'
        ],
        borderColor: [
            'rgba(255,99,132,1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)',
            'rgba(255, 0, 0)',
            'rgba(0, 255, 0)',
            'rgba(0, 0, 255)',
            'rgba(192, 192, 192)',
            'rgba(255, 255, 0)',
            'rgba(255, 0, 255)'
        ],
        borderWidth: 1,
        data: yValues
    }]
};
var options = {
    maintainAspectRatio: false,
    scales: {
        yAxes: [{
            ticks: {
                min: 0,
                beginAtZero: true
            },
            gridLines: {
                display: true,
                color: "rgba(255,99,164,0.2)"
            }
        }],
        xAxes: [{
            ticks: {
                min: 0,
                beginAtZero: true
            },
            gridLines: {
                display: false
            }
        }]
    }
};




    
    var resetCanvas = function () {
        $('#barChart').remove(); // this is my <canvas> element
        $('#barChart-container').append('<canvas id="barChart" style="width:100%; height:500px"></canvas>');
        canvas = document.querySelector('#barChart');
        ctx = canvas.getContext('2d');
        ctx.canvas.width = $('#graph').width(); // resize to parent width
        ctx.canvas.height = $('#graph').height(); // resize to parent height
        var x = canvas.width / 2;
        var y = canvas.height / 2;
        ctx.font = '10pt Verdana';
        ctx.textAlign = 'center';
        ctx.fillText('This text is centered on the canvas', x, y);
    };

    $.getJSON('/Admin/GetDataSet', { get_param: '' }, function (data) {
       
        $.each(data, function (index, element) {

            $.each(element.dataSets, function (index, element) {
                labels.push(element.dimensionOne);
                yValues.push(element.quantity);
            });
           

            
        });
        
        
        DrawChart();
        
        
        
    });

    function DrawChart() {
        var chart = new Chart(ctx, {
            options: options,
            data: data,
            type: 'bar'
        })
        chart.update();
            };
  

    var dataType = 'application/json; charset=utf-8';

    $('#userOptionDropdown').change(function () {
        var username = $(this).val();
        $("#userOptionsForm").ajaxSubmit({
            url: '/Admin/GetDataSet',
            type: 'post',
            contentType: dataType,
            data: {
                userToShow: username,
                monthToShow: $("#availableMonthsDropdown").val()
            },
            success: function (data) {
                
                resetCanvas();
                yValues.length = 0;
                
                $.each(data, function (index, element) {
                    $.each(element.dataSets, function (index, element) {
                        yValues.push(element.quantity);
                    });
                });
                
               
                
                DrawChart();
                
                

            }
        });

    });
    $('#availableMonthsDropdown').change(function () {
        var month = $(this).val();
        $("#availableMonthsForm").ajaxSubmit({
            url: '/Admin/GetDataSet',
            type: 'post',
            contentType: dataType,
            data: {
                userToShow: $("#userOptionDropdown").val(),
                monthToShow: month
            },
            success: function (data) {

                resetCanvas();
                yValues.length = 0;

                $.each(data, function (index, element) {
                    $.each(element.dataSets, function (index, element) {
                        yValues.push(element.quantity);
                    });
                });



                DrawChart();



            }
        });

    });
   
});


////Testa 



//    var data = {
//        labels: labels,
//    datasets: [{
//        label: "Countries Chart",
//        backgroundColor: [
//            'rgba(255, 99, 132, 0.2)',
//            'rgba(54, 162, 235, 0.2)',
//            'rgba(255, 206, 86, 0.2)',
//            'rgba(75, 192, 192, 0.2)',
//            'rgba(153, 102, 255, 0.2)',
//            'rgba(255, 159, 64, 0.2)',
//            'rgba(255, 0, 0)',
//            'rgba(0, 255, 0)',
//            'rgba(0, 0, 255)',
//            'rgba(192, 192, 192)',
//            'rgba(255, 255, 0)',
//            'rgba(255, 0, 255)'
//        ],
//        borderColor: [
//            'rgba(255,99,132,1)',
//            'rgba(54, 162, 235, 1)',
//            'rgba(255, 206, 86, 1)',
//            'rgba(75, 192, 192, 1)',
//            'rgba(153, 102, 255, 1)',
//            'rgba(255, 159, 64, 1)',
//            'rgba(255, 0, 0)',
//            'rgba(0, 255, 0)',
//            'rgba(0, 0, 255)',
//            'rgba(192, 192, 192)',
//            'rgba(255, 255, 0)',
//            'rgba(255, 0, 255)'
//        ],
//        borderWidth: 1,
//        data: yValues,
//        }]
//};
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
//    function DrawChart() {
//        var chart = new Chart(ctx, {
//            options: options,
//            data: data,
//            type: 'bar'

//        });
//        chart.update();
//        return chart;

//    }
        






















