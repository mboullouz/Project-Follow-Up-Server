/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
class MainApp {
    constructor() {
        console.log("module app loaded !");
        $(".btn").click(function (event) {
            console.log("clicked");
        });
        $('.confirm').click(this.handleClickConfirm);
        $('.btn').click(function (evt) { confirm('conf  ?'); });
        $('.table').addClass('table-bordered');
        $('.btn').addClass('btn-primary');
       // var p = new Project.Pr();
       
    }
    handleClickConfirm = (evt) => {
        var conf = confirm("This action is dangerous, do you confirm ?");
        return conf;
    }
}
var mainApp = new MainApp();