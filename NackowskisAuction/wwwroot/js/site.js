// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//LoginModal ajax
$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('a[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            // find IsValid input field and check it's value
            // if it's valid then hide modal window
            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
        });
    });

});

// attach click event handler to an element
// which is located inside #modal-placeholder
// and has data-save attribute equal to modal





//Barchart dropdown select ajax calls
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
