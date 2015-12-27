
module Project {
   

    export class ApiCall {
        constructor() {
            console.log('Api class caller is loaded!');
        }
        getData(url: string, data: any) {
            $.ajax({
                method: "GET",
                url   : url,
                data  : { name: "John", location: "Boston" }
            })
                .done(function (msg) {
                    $('#notif').text('Operation done! ');
                    $('#notif').click(function (evt) { $('#notif').hide(); });
            });
        }
    }

    export class Pr {
        apiCall: ApiCall = new ApiCall();
        constructor() {
            console.log('Project TS loaded successfully');
            $('#callServer').click(this.handleClickCallServer);
        }
        handleClickCallServer = (event) => {
            this.apiCall.getData('http://localhost:53073/Project/Js/4', {});
        }
    }
}
 