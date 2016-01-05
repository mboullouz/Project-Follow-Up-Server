/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
/// <reference path="./app/Shared/Models/Entity.ts" />

/**
 * Manage notification UI
 */
class Notification {
    constructor() {
        console.log("notification Ui loaded");

        $(".notificationElement").click(
            function () {
                //var element = event.target;
                var idClicked = $(this).data("idnotif");
                console.log("id notif " + idClicked);
            }
        );


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
            return false
        });
    }
}
class MainApp {
    constructor() {
        console.log("module: MainApp !");
        var task = new Entity.Task();
        var notif = new Notification();
        this.plugConfirm();
    }
    plugConfirm() {
        $('.confirm').click(this.handleClickConfirm);
    }
    handleClickConfirm = (evt) => {
        return confirm("This action is dangerous, do you confirm ?");
    }
}
var mainApp = new MainApp();
 