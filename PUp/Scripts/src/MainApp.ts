/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
/// <reference path="./app/Shared/Models/Entity.ts" />

/**
 * Manage notification UI
 */
class Notification {
    private httpPost: HttpCall.PostCall = new HttpCall.PostCall();
    private markNotificationSeenUrl: string;
    constructor() {
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
            return false
        });
    }
    private handleSeenClick = (event) => {
        var element = event.target;
        var idClicked = $(event.currentTarget).data("idnotif");
       
        console.log("id curr target notif: " + idClicked);
        this.markSeen(idClicked);

        $(event.target).parent("div").hide();
    }
    public markSeen(id: any) {
        var notification = id/*new Entity.NotificationBasic(id, true)*/;
        this.httpPost.send(notification, this.markNotificationSeenUrl+id, this.onSuccess, HttpCall.GenericRespnse.onError,"DELETE");
    }
    onSuccess(response) {
        console.log("Response >>  \n" + JSON.stringify(response));
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
 