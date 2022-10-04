app.controller("MasajidManagerController", function ($scope, $compile, $timeout, $http) {

    $scope.masajid = {
        Search: ''
        //,
        //CompanyID: ''
    };

    $scope.SearchMasajid = function () {
        BindMasajidGrid();
    };

    BindMasajidGrid();

    //$scope.AddMasjid = function () {
    //    $http({
    //        method: "post",
    //        url: "/Masajid/AddUpdateMasjid",
    //        data: {},
    //        dataType: "json"
    //    }).then(function (msg) {
    //        angular.element("#modalLabel").text("Add Masjid");
    //        angular.element("#modalBody").html(msg.data);
    //        $.validator.unobtrusive.parse("#resturantForm");
    //        angular.element("#detailsModal").modal("show");
    //    }); 
    //};

    $scope.AddMasjid = function () {
        $http({
            method: "get",
            url: "/Masajid/AddUpdateMasjid?masjidId=",
            data: {},
            dataType: "get"
        }).then(function (msg) {
            angular.element("#modalLabel").text("Add Masjid");
            angular.element("#modalBody").html(msg.data);
            $.validator.unobtrusive.parse("#masjidForm");
            angular.element("#detailsModal").modal("show");
        });
    };

    $scope.EditMasjid = function (recordId) {
        $http({
            method: "get",
            url: "/Masajid/AddUpdateMasjid?masjidId=" + recordId,
            //data: { masjidId: recordId },
            data: { },
            dataType: "json"
        }).then(function (msg) {
            angular.element("#modalLabel").text("Edit Masjid");
            angular.element("#modalBody").html(msg.data);
            $.validator.unobtrusive.parse("#masjidForm");
            angular.element("#detailsModal").modal("show");

            //$("input:file").each(function (index, item) {
            //    var parent = $(this).parents(".file-upload-control").first();
            //    if (parent.find("input[type='hidden']").val() != "") {
            //        parent.find("span.has-success > span.form-control-feedback").text(parent.find("input[type='hidden']").val());
            //    }
            //});
        });
    };

    $scope.UploadCSV = function (masjidId) {
        $http({
            method: "get",
            url: "/Masajid/UploadPrayerTimings?masjidId=" + masjidId,
            //data: { masjidId: masjidId },
            data: {},
            dataType: "json"
        }).then(function (msg) {
            angular.element("#modalLabel").text("Upload Prayer Timings");
            angular.element("#modalBody").html(msg.data);
            $.validator.unobtrusive.parse("#prayerTimingForm");
            angular.element("#detailsModal").modal("show");
        });
    };
    
    function BindMasajidGrid() {

        //$scope.orderSearch.CompanyID = $("#hdfModelUserID").val();

        $("#tblMasajid").advancedDataTable({
            url: "/Masajid/FetchMasajid",
            postData: $scope.masajid,
            bindingData: [
                //{
                //    "render": function (data, type, row) {
                //        return GetLocaleDateTime(row.TimeStamp);
                //    }
                //},
                { "data": "Name" },
                { "data": "Offset" },
                { "data": "Fajar" },
                { "data": "Zohar" },
                { "data": "Asar" },
                { "data": "Magrib" },
                { "data": "Isha" },
                { "data": "Juma" },
                {
                    "render": function (data, type, row) {
                        return "<input type='hidden' id='hdfMasjidId' value='" + row.Id + "' /> <a href='javascript: void(0);' title='Upload CSV to Update Prayer Timing' onclick='UploadCSV(this)'>Upload CSV</a> | <a href='javascript: void(0);' title='Edit' onclick='EditMasjid(this)'>Edit</a>";
                    }
                }
            ]
        });
    }

});

function EditMasjid(e) {
    var masjidId = $(e).parent().find("#hdfMasjidId").val();
    angular.element(document.getElementById('DivItems')).scope().EditMasjid(masjidId);
}

function UploadCSV(e) {
    var masjidId = $(e).parent().find("#hdfMasjidId").val();
    angular.element(document.getElementById('DivItems')).scope().UploadCSV(masjidId);
}