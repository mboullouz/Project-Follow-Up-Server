/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
class MainApp {
    constructor() {
        console.log("module: MainApp !");
        this.plugConfirm();
    }
    plugConfirm() {
        $('button').click(this.handleClickConfirm);
    }
    handleClickConfirm = (evt) => {
        return confirm("This action is dangerous, do you confirm ?");
    }
}
var mainApp = new MainApp();
 