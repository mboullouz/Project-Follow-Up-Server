var Project;
(function (Project) {
    var ApiCall = (function () {
        function ApiCall() {
            console.log('Api class caller is loaded!');
        }
        ApiCall.prototype.getData = function (url, data) {
            $.ajax({
                method: "GET",
                url: url,
                data: { name: "John", location: "Boston" }
            })
                .done(function (msg) {
                $('#notif').text('Operation done! ');
                $('#notif').click(function (evt) { $('#notif').hide(); });
            });
        };
        return ApiCall;
    })();
    Project.ApiCall = ApiCall;
    var Pr = (function () {
        function Pr() {
            var _this = this;
            this.apiCall = new ApiCall();
            this.handleClickCallServer = function (event) {
                _this.apiCall.getData('http://localhost:53073/Project/Js/4', {});
            };
            console.log('Project TS loaded successfully');
            $('#callServer').click(this.handleClickCallServer);
        }
        return Pr;
    })();
    Project.Pr = Pr;
})(Project || (Project = {}));
