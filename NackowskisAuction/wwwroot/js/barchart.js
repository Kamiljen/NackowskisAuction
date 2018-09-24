var labels = [];
var yValues = [];
var speed = 250;
var charts = [];
var value = 0;
var samples = 100;
var ctx = document.getElementById("chart").getContext('2d');

var data = {
    labels: labels,
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
        data: yValues,
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

var chart = new Chart(ctx, {
    options: options,
    data: data,
    type: 'bar'

});

$(document).ready(function () {
    

    $.getJSON('/Admin/GetDataSet', { get_param: '' }, function (data) {

        $.each(data, function (index, element) {

            $.each(element.dataSets, function (index, element) {
                labels.push(element.dimensionOne);
                yValues.push(element.quantity);
            });
           

            chart.update();
        });
       
        
    });
    

        //var myChart = new Chart(ctx,
        //    {
        //        type: 'bar',
        //        data: {
        //            labels: labels,
        //            datasets: [
        //                {
        //                    data: yValues,
        //                    backgroundColor: [
        //                        'rgba(255, 99, 132, 0.2)',
        //                        'rgba(54, 162, 235, 0.2)',
        //                        'rgba(255, 206, 86, 0.2)',
        //                        'rgba(75, 192, 192, 0.2)',
        //                        'rgba(153, 102, 255, 0.2)',
        //                        'rgba(255, 159, 64, 0.2)',
        //                        'rgba(255, 0, 0)',
        //                        'rgba(0, 255, 0)',
        //                        'rgba(0, 0, 255)',
        //                        'rgba(192, 192, 192)',
        //                        'rgba(255, 255, 0)',
        //                        'rgba(255, 0, 255)'
        //                    ],
        //                    borderColor: [
        //                        'rgba(255,99,132,1)',
        //                        'rgba(54, 162, 235, 1)',
        //                        'rgba(255, 206, 86, 1)',
        //                        'rgba(75, 192, 192, 1)',
        //                        'rgba(153, 102, 255, 1)',
        //                        'rgba(255, 159, 64, 1)',
        //                        'rgba(255, 0, 0)',
        //                        'rgba(0, 255, 0)',
        //                        'rgba(0, 0, 255)',
        //                        'rgba(192, 192, 192)',
        //                        'rgba(255, 255, 0)',
        //                        'rgba(255, 0, 255)'
        //                    ],
        //                    borderWidth: 1
        //                }
        //            ]
        //        },
        //        options: {
        //            maintainAspectRatio: false,
        //            scales: {
        //                yAxes: [{
        //                    ticks: {
        //                        min: 0,
        //                        beginAtZero: true
        //                    },
        //                    gridLines: {
        //                        display: true,
        //                        color: "rgba(255,99,164,0.2)"
        //                    }
        //                }],
        //                xAxes: [{
        //                    ticks: {
        //                        min: 0,
        //                        beginAtZero: true
        //                    },
        //                    gridLines: {
        //                        display: false
        //                    }
        //                }]
        //            }
        //        }
        //    });



    
        
        

    
   

    //function addData(chartID, label, data) {
    //    var ctx = document.getElementById(chartID).getContext('2d');
    //    var newChartData = {};
    //    newChartData.type = "bar";
    //    newChartData.data = {};
    //    newChartData.data.labels = labels;
    //    newChartData.data.datasets = [];
    //    //$.each(data.data.dataSets, function(index, element) {
    //    //    var temp = element.quantity;
    //    //    $.each(temp, function(index, element) {
    //    //        var tempi = element;
    //    //        newChartData.data.datasets.push(element.quantity);
    //    //    });
    //    //});
    //    newChartData.data.datasets.push(data);
    //    //var myChart = new Chart(ctx, newChartData);
    //    var chart = new Chart(ctx, {
    //        options: options,
    //        data: data,
    //        type: 'bar'

    //    });
    //    chart.update();
    //}
    
    
   
    function removeData(chart) {
        chart.data.labels.pop();
        var temp = chart.data;
        Array.prototype.forEach.call(chart.data.children, child => {
            console.log(child);
        });
        //chart.data.datasets.foreEach((dataset) => {
        //    dataset.data.pop();
        //});
        
        chart.update();
    } 

    function addData(chart, label, data) {
        chart.data.labels.push(label);
        chart.data.datasets.foreEach((dataset) => {
            dataset.data.push(data);
        });
        chart.update();
    } 
    




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
                
                    removeData(chart);
                
                
                $.each(data, function (index, element) {
                    $.each(element.dataSets, function (index, element) {
                        addData(chart, element.dimensionOne, element.quantity);
                    });
                });
                
               
                    chart.update();
                
                

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
                removeData(ctx);
                $.each(data, function (index, element) {

                    $.each(element.dataSets, function (index, element) {
                        addData(ctx, element.dimensionOne, element.quantity);

                    });
                    


                });
           
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
        






















