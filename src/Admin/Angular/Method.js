
function PostMethod(url, data) {

    return $.ajax({
        method: "Post",
        url: url,
       // dataType: 'json',
         contentType: 'application/json',
        data: JSON.stringify(data)
    });
}

function GetMethod(url) {

    return $.ajax({
        method: "Get",
        url: url
    });
}

function PutMethod(url, data) {

    return $.ajax({
        method: "Put",
        url: url,
        data: data
    });

}

function DeleteMethod(url) {

    return $.ajax({
        method: "Delete",
        url: url
    });

}

//$(function () {
//    $(".fromDate").datepicker({
//        changeMonth: true,
//        dateFormat: 'mm/dd/yy',
//        changeYear: true,
//        day: 0,
//        onSelect: function (selected) {
//            $(".toDate").datepicker("option", "minDate", selected)
            
//        }
//    });
//    $(".toDate").datepicker({
//        changeMonth: true,
//        dateFormat: 'mm/dd/yy',
//        changeYear: true,
//        day: 0,
//        onSelect: function (selected) {
//            $(".fromDate").datepicker("option", "maxDate", selected)
//        }
//    });

   
//});
