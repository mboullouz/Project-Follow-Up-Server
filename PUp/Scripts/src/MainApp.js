/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
/// <reference path="./app/Shared/Models/Entity.ts" />
/**
 * Manage notification UI
 */
var Notification = (function () {
    function Notification() {
        var _this = this;
        this.httpPost = new HttpCall.PostCall();
        this.handleSeenClick = function (event) {
            var element = event.target;
            var idClicked = $(event.currentTarget).data("idnotif");
            console.log("id curr target notif: " + idClicked);
            _this.markSeen(idClicked);
            $(event.target).parent("div").hide();
        };
        console.log("notification Ui loaded");
        this.markNotificationSeenUrl = "/api/NotificationApi/" /*$("#conf-notif").data("urlmarkseen")*/;
        console.log("url: " + this.markNotificationSeenUrl);
        $(".notificationElement").click(this.handleSeenClick);
        $("#notificationLink").click(function () {
            $("#notificationContainer").fadeToggle(300);
            $("#notification_count").fadeOut("slow");
            return false;
        });
        //Document Click
        $(document).click(function () {
            $("#notificationContainer").hide();
        });
        //Popup Click
        $("#notificationContainer").click(function () {
            return false;
        });
    }
    Notification.prototype.markSeen = function (id) {
        var notification = id;
        this.httpPost.send(notification, this.markNotificationSeenUrl + id, this.onSuccess, HttpCall.GenericRespnse.onError, "DELETE");
    };
    Notification.prototype.onSuccess = function (response) {
        console.log("Response >>  \n" + JSON.stringify(response));
    };
    return Notification;
})();
var MainApp = (function () {
    function MainApp() {
        this.handleClickConfirm = function (evt) {
            return confirm("This action is dangerous, do you confirm ?");
        };
        console.log("module: MainApp !");
        var task = new Entity.Task();
        var notif = new Notification();
        this.plugConfirm();
    }
    MainApp.prototype.plugConfirm = function () {
        $('.confirm').click(this.handleClickConfirm);
    };
    return MainApp;
})();
var mainApp = new MainApp();
//# sourceMappingURL=MainApp.js.map