$(function () {
    //toastr.options = {
    //    "closeButton": false,
    //    "debug": false,
    //    "newestOnTop": false,
    //    "progressBar": true,
    //    "positionClass": "toast-bottom-full-width",//"toast-top-full-width",//"toast-bottom-right",
    //    "preventDuplicates": false,
    //    "onclick": null,
    //    "showDuration": "3000",
    //    "hideDuration": "1000",
    //    "timeOut": "10000",
    //    "extendedTimeOut": "1000",
    //    "showEasing": "swing",
    //    "hideEasing": "linear",
    //    "showMethod": "fadeIn",
    //    "hideMethod": "fadeOut"
    //}


    setInterval(function GetCurrentTime() {

        var time = new Date();
        $(".current-time").val((time.getHours().toString().length < 2 ? "0" + time.getHours() : time.getHours()) + " : " + (time.getMinutes().toString().length < 2 ? "0" + time.getMinutes() : time.getMinutes()) + " : " + (time.getSeconds().toString().length < 2 ? "0" + time.getSeconds() : time.getSeconds()));

    }, 1000);


});

function GetLocaleDateTime(utcDateTime) {
    var temp = new Date(utcDateTime + " UTC");

    var date = GetTwoDigitNumber(temp.getDate()) + '/' + GetTwoDigitNumber(temp.getMonth() + 1) + '/' + temp.getFullYear();
    var time = GetTwoDigitNumber(temp.getHours()) + ":" + GetTwoDigitNumber(temp.getMinutes()); //+ ":" + GetTwoDigitNumber(temp.getSeconds());
    var dateTime = date + ' ' + time;

    return dateTime;
}

function GetLocaleDate(utcDateTime) {
    var temp = new Date(utcDateTime + " UTC");
    return GetTwoDigitNumber(temp.getDate()) + '/' + GetTwoDigitNumber(temp.getMonth() + 1) + '/' + temp.getFullYear();
}

function GetTwoDigitNumber(num) {
    return ("0" + num).slice(-2);
}


function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function isFloat(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    } else {
        // If the number field already has . then don't allow to enter . again.
        if (evt.target.value.search(/\./) > -1 && charCode == 46) {
            return false;
        }
        return true;
    }
}
function isLength(evt) {
    //$("#length").attr('maxlength', '2');
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {


        return false;
    } else {

        // If the number field already has . then don't allow to enter . again.
        if (evt.target.value.search(/\./) > -1 && charCode == 46) {
            $("#length").attr('maxlength', '4');

            return false;
        }
        else if (charCode == 46) {

            $("#length").attr('maxlength', '4');

        }
        return true;
    }
}

//$('input').bind('keypress', function (event) {

//    var regex = new RegExp("^[a-zA-Z0-9]+$");
//    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
//    if (!regex.test(key)) {
//        event.preventDefault();
//        return false;
//    }
//});

LoaderShow = function () {
    $("#divLoader").removeClass("showNone");
}
LoaderHide = function () {
    $("#divLoader").addClass("showNone");
}
GetAccountCode = function () {
    return $('#accountCode').val();
}
SetAccountCode = function (accountCode) {
    $('#accountCode').val(accountCode);
}
//$(function () {
//    $('.clientSearchTextBox').autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/Invoice/FetchClients",
//                data: "{ 'prefix': '" + request.term + "'}",
//                dataType: "json",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                success: function (data) {

//                    response($.map(data, function (item) {

//                        return {
//                            label: item.Name,
//                            val: item.Id
//                        }
//                    }))

//                },
//                error: function (response) {
//                },
//                failure: function (response) {
//                }
//            });
//        },

//        change: function (event, ui) {
//            if (ui.item == null || ui.item == undefined) {
//                $(".clientSearchTextBox").val("");
//                $('#clientId').val("");

//            }
//        },

//        select: function (e, i) {
//            $('#clientId').val(i.item.val);

//        },
//        minLength: 1
//    });

//    $('.productTextBox').autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "/Invoice/FetchProducts",
//                data: "{ 'prefix': '" + request.term + "'}",
//                dataType: "json",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                success: function (data) {
//                    response($.map(data, function (item) {
//                        return {
//                            label: item.Name,
//                            val: item.ID,
//                            price: item.Price,
//                            cost: item.UnitCost,
//                            desc: item.Description
//                        }
//                    }))

//                },
//                error: function (response) {
//                },
//                failure: function (response) {
//                }
//            });
//        },

//        change: function (event, ui) {
//            if (ui.item == null || ui.item == undefined) {
//                $('#hdfItemId').val("");
//                $('#txtProductName').val("");

//            }
//        },

//        select: function (e, i) {
//            $('#hdfItemId').val(i.item.val);
//            $("#txtUnitPrice").val(i.item.price);
//            $("#txtUnitCost").val(i.item.cost);
//            $("#txtDescription").val(i.item.desc);
//            $("#txtQuantity").val(1);
//            $("#itemContents").removeClass("showNone");

//        },
//        minLength: 0
//    });
//});

$('.number').keypress(function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8)) {
        event.preventDefault();
    }
});
$(document).on('keydown', '.neg', function (e) {


    if (!((e.keyCode > 95 && e.keyCode < 106)
        || (e.keyCode > 47 && e.keyCode < 58)
        || e.keyCode == 8)) {

        return false;

    }

    //else if ($('.neg').val().length > 2) {

    //    return false;
    //}


})
$(document).on('keydown', '.length', function (e) {
    var data = $('.length').val();


    //if (!((e.keyCode > 95 && e.keyCode < 106)
    // || (e.keyCode > 47 && e.keyCode < 58)
    // || e.keyCode == 8)) {

    //    return false;

    //}

    //else if ($('.neg').val().length > 2) {

    //    return false;
    //}


})
$(document).on("keypress", ".jsgrid-align-Quantity input", function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8)) {
        event.preventDefault();
    }
});

$(document).on("keypress", ".decimal", function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8)) {
        event.preventDefault();
    }
});
$(document).on("keypress", ".datepicker", function (event) {
    return false;
});
function ConvertToDecimal(value) {
    value = value.toString();
    if (value.indexOf(".") >= 0) {
        var floatValue = value.substring(0, value.indexOf(".") + 3);
        return parseFloat(floatValue);
    }
    else {
        return parseFloat(value);
    }
};
function Print() {
    var prtContent = document.getElementById('DivPrint');
    var WinPrint = window.open('', '', 'left=0,top=0,width=780,height=600,toolbar=0,scrollbars=1,status=1');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();
    prtContent.innerHTML = strOldOne;

};
function ClearDate(domObj) {
    $(domObj.nextElementSibling).val("");
}

function SuccessMessage(msg) {
    toastr.success(msg);
}

function ErrorMessage(msg) {
    toastr.error(msg);
}
function WarningMessage(msg) {
    toastr.warning(msg);
}
function InfoMessage(msg) {
    toastr.info(msg);
}
function ShowMessage(msg) {
    if (msg.Info) {
        InfoMessage(msg.MessageDetail);
    }
    else if (msg.Warning) {
        WarningMessage(msg.MessageDetail);
    }
    else if (msg.Success) {
        SuccessMessage(msg.MessageDetail);
    }
    else if (!msg.Success) {
        ErrorMessage(msg.MessageDetail);
    }
    else if (msg.Info) {
        InfoMessage(msg.MessageDetail);
    }
    else if (msg.Warning) {
        WarningMessage(msg.MessageDetail);
    }
}
//var CurrencySymbol = "";
//GetMethod("/Generic/CompanyInfo").then(function (d) { CurrencySymbol = " (" + d + ") "; })

$(document).on("keypress", ".numeric-whole", function (evt) {
    return isNumber(evt);
});

$(document).on("keypress", ".numeric-float", function (evt) {
    return isFloat(evt);
});

GetProductSKU = function () {
    return $('#productSKU').val();
}
SetProductSKU = function (productSKU) {
    $('#productSKU').val(productSKU);
}

/*Customized Plugins*/
$.fn.advancedDataTable = function (options) {
    if ($.fn.dataTable.isDataTable(this))
        $(this).dataTable().fnPageChange($(this).dataTable().fnPagingInfo().iPage);
    else {
        $(this).dataTable().fnDestroy();
        $(this).DataTable({
            "processing": true,
            "bSort": true,
            "bFilter": false,
            "bDeferRender": true,
            //  "dom": 'Blfrtip',


            // lengthMenu: [
            //  [10, 25, 50, -1],
            //  ['10 rows', '25 rows', '50 rows', 'Show all']
            // ],
            //buttons: [
            // 'pageLength'
            //],

            "pagingType": "full_numbers",

            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],



            //   "columnDefs": [
            //  { "width": "20%", "targets": 0 }
            // ],

            "oLanguage": {
                "sSearch": "",
                "sSearchPlaceholder": "Search...",
                "sProcessing": '<img src="/Assets/common/img/ajax-loader.gif" style="display:block; margin: 0 auto; width:200px;"/>',
                "sLengthMenu": "_MENU_",
                "oPaginate":
                {
                    "sNext": '→',
                    "sLast": 'Last »',
                    "sFirst": '« First',
                    "sPrevious": '←'
                }
            },
            "initComplete": function (settings, json) {
                $("[name=" + $(this).attr("id") + "_length]").addClass("form-control pull-right");
                //$("[name=" + $(this).attr("id") + "_length]").detach().prependTo(".manual-dropdown");
                //$(".dataTables_length").remove();
                // $("[name=" + $(this).attr("id") + "_length]").addClass("form-control");
                //$('.i-checks').iCheck({
                //    checkboxClass: 'icheckbox_square-blue',
                //    radioClass: 'iradio_square-blue'
                //});
                //$('.i-checks').iChecked();
            },
            "serverSide": true,
            "ajaxSource": options.url,
            //fnServerData method used to inject the parameters into the AJAX call sent to the server-side
            "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
                //BlockUI();

                aoData.push({ "name": "SearchJson", "value": JSON.stringify(options.postData) }); // Add some extra data to the sender


                $.getJSON(sSource, aoData, function (d) {
                    /* Do whatever additional processing you want on the callback, then tell DataTables */

                    fnCallback(d.Data);

                }).error(function (d, c, dd, t) {
                    if (d.status == 200) //{ }
                        AccessDenied();
                    else
                        ErrorMessage("Something went wrong, please try again");
                });
            },
            "bResetDisplay": false,
            "bLengthChange": true,
            "aaSorting": [],
            //"createdRow": options.createdRow,
            "drawCallback": function (settings) {
                //$('.i-checks').iCheck({
                //    checkboxClass: 'icheckbox_square-blue',
                //    radioClass: 'iradio_square-blue'
                //});
            },
            "columns": options.bindingData,
            "aoColumnDefs": options.disableSorting
        });
    }
};
$.fn.iChecked = function () {
    return $(this).prop("checked");
};

/*Prototypes*/
String.prototype.parseBoolean = function () {
    return ("true" == this.toLowerCase()) ? true : false
}
String.prototype.IsNullOrEmpty = function () {
    return ((this == "" || this == null) ? true : false)
}

jQuery.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
    return {
        "iStart": oSettings._iDisplayStart,
        "iEnd": oSettings.fnDisplayEnd(),
        "iLength": oSettings._iDisplayLength,
        "iTotal": oSettings.fnRecordsTotal(),
        "iFilteredTotal": oSettings.fnRecordsDisplay(),
        "iPage": oSettings._iDisplayLength === -1 ?
            0 : Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),
        "iTotalPages": oSettings._iDisplayLength === -1 ?
            0 : Math.ceil(oSettings.fnRecordsDisplay() / oSettings._iDisplayLength)
    };
};