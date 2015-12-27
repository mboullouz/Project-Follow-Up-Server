/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
class App {
    constructor() {
        console.log("module app loaded !");
        $(".btn").click(function (event) {
            console.log("clicked");
        });
        $('.table').addClass('table-bordered');
        $('.btn').addClass('btn-primary');
        var p = new Project.Pr();
       
    }

}

var app = new App();
