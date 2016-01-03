/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
/// <reference path="./app/Shared/Models/Entity.ts" />
class MainApp {
    constructor() {
        console.log("module: MainApp !");
        var task = new Entity.Task();
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
 