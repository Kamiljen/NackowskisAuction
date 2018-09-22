// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$('#datepicker').datetimepicker({
//    uiLibrary: 'bootstrap4',
//    modal: true,
//    footer: true,
//    format: 'dd-mm-yyyy HH'
//});
//var today, datetimepicker;
//today = new Date(new Date().getDate(), new Date().getMonth(), new Date().getFullYear(), new Date().getHours());
//datepicker = $('#datepicker').datetimepicker({
//    minDate: today,
//    uiLibrary: 'bootstrap4',
//    modal: true,
//    footer: true,
//    format: 'dd-mm-yyyy HH'
//});
//var today = new Date();
//var dd = today.getDate();
//var mm = today.getMonth() + 1; //January is 0!
//var HH = today.getHours();


//var yyyy = today.getFullYear();
//if (dd < 10) {
//    dd = '0' + dd;
//}
//if (mm < 10) {
//    mm = '0' + mm;
//}
//if (HH < 10) {
//    HH = '0' + HH + ':' + '00';
//}
//today = dd + '-' + mm + '-' + yyyy + " " + HH;

//$('#fromDatepicker').datetimepicker({
//    uiLibrary: 'bootstrap4',
//    format: 'dd-mm-yyyy HH:MM',
//    value: today,
//    minDate: '16-09-2018 12.00',
//    modal: true,
//    footer: true

//});
//$('#toDatepicker').datetimepicker({
//    uiLibrary: 'bootstrap4',
//    format: 'dd-mm-yyyy HH:MM',
//    value: today,
//    minDate: '16-09-2018 12:00',
//    modal: true,
//    footer: true

//});

//$('#datepicker').datepicker({
//    uiLibrary: 'bootstrap4',
//    format: 'yyyy-mm-dd',
//    weekStartDay: 1,
//    minDate: "2018-09-16",
//    maxDate: "2018-09-23"
//});

//var frm = $('#userOptionsForm');

//frm.submit(function (e) {

//    e.preventDefault();

//    $.ajax({
//        type: frm.attr('method'),
//        url: frm.attr('action'),
//        data: frm.serialize(),
//        success: function (data) {
//            console.log('Submission was successful.');
//            console.log(data);
//        },
//        error: function (data) {
//            console.log('An error occurred.');
//            console.log(data);
//        },
//    });
//});
var dataType = 'application/json; charset=utf-8';

$('#userOptionDropdown').change(function () {
    var username = $(this).val();
    $("#userOptionsForm").ajaxSubmit({
        url: '/Admin/LineChart',
        type: 'post',
        contentType: dataType,
        data: {
            userToShow: username,
            monthToShow: $("#availableMonthsDropdown").val()
        },
        success: function (data) {
            $('#lineChartDiv').empty();
            $('#lineChartDiv').html(data);
        }
    });
    
});
$('#availableMonthsDropdown').change(function () {
    var month = $(this).val();
    $("#availableMonthsForm").ajaxSubmit({
        url: '/Admin/LineChart',
        type: 'post',
        contentType: dataType,
        data: {
            userToShow: $("#userOptionDropdown").val(),
            monthToShow: month
        },
        success: function (data) {
            $('#lineChartDiv').empty();
            $('#lineChartDiv').html(data);
        }
    });

});

$('#orderByDropdown').change(function () {
    var orderBy = $(this).val();
    $("#orderByForm").ajaxSubmit({
        url: '/Home/FindAcutionsWithParams',
        type: 'post',
        contentType: dataType,
        data: {
            OrderBy : orderBy,
            SearchParam: $("#searchParamsDropdown").val(),
            SearchString: $("#searchInput").val()
        },
        success: function (data) {
            
            $('#searchList2').empty();
            $('#searchList2').html(data);
        }
    });

});

$('#searchParamsDropdown').change(function () {
    var searchParams = $(this).val();
    $("#orderByForm").ajaxSubmit({
        url: '/Home/FindAcutionsWithParams',
        type: 'post',
        contentType: dataType,
        data: {
            OrderBy: $("#orderByDropdown").val(),
            SearchParam: searchParams,
            SearchString: $("#searchInput").val()
        },
        success: function (data) {
            $('#searchList2').empty();
            $('#searchList2').html(data);
        }
    });

});

$('#searchSubmit').click(function () {
   
    $("#orderByForm").ajaxSubmit({
        url: '/Home/FindAcutionsWithParams',
        type: 'post',
        contentType: dataType,
        data: {
            OrderBy: $("#orderByDropdown").val(),
            SearchParam: $("#searchParamDropdown").val(),
            SearchString: $("#searchInput").val()
        },
        success: function (data) {
            $('#searchList2').empty();
            $('#searchList2').html(data);
        }
    });

});
