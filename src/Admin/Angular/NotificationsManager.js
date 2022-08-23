app.controller("NotificationsManagerController", function ($scope, $compile, $timeout, $http) {

    $scope.notification = {
        Search: ''
        //,
        //CompanyID: ''
    };

    $scope.SearchNotifications = function () {
        BindNotificationsHistoryGrid();
    };

    BindNotificationsHistoryGrid();

    //$scope.ViewNotificationDetails = function (OrderID) {
    //    $http({
    //        method: "post",
    //        url: "/Home/ViewNotification",
    //        data: { OrderID: OrderID },
    //        dataType: "json"
    //    }).then(function (msg) {
    //        angular.element("#modalLabel").text("Order Details");
    //        angular.element("#modalBody").html(msg.data);
    //        angular.element("#detailsModal").modal("show");
    //    });
    //};

    function BindNotificationsHistoryGrid() {

        //$scope.orderSearch.CompanyID = $("#hdfModelUserID").val();

        $("#tblNotifications").advancedDataTable({
            url: "/Home/FetchNotifications",
            postData: $scope.notification,
            bindingData: [
                {
                    "render": function (data, type, row) {
                        return GetLocaleDateTime(row.TimeStamp);
                    }
                },
                //{ "data": "TimeStamp" },
                { "data": "Title" },
                { "data": "Description" }
                //,
                //{
                //    "render": function (data, type, row) {
                //        return "<input type='hidden' id='hdfNotificationId' value='" + row.Id + "' /> <a href='javascript: void(0);' title='Order Details' onclick='ViewNotificationDetails(this)'>View Notification Details</a>";
                //    }
                //}
            ]
        });
    }

});

//function ViewNotificationDetails(e) {
//    var OrderID = $(e).parent().find("#hdfNotificationId").val();
//    angular.element(document.getElementById('DivItems')).scope().ViewNotificationDetails(OrderID);
//}