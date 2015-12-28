/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="./app/Project.ts" />
var App = (function () {
    function App() {
        console.log("module app loaded !");
        $(".btn").click(function (event) {
            console.log("clicked");
        });
        $('.table').addClass('table-bordered');
        $('.btn').addClass('btn-primary');
        var p = new Project.Pr();
    }
    return App;
})();
//var app = new App();
//# sourceMappingURL=App.js.map