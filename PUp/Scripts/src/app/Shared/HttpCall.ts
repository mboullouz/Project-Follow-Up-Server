/// <reference path="../../../typings/jquery/jquery.d.ts" />
module HttpCall{
    export class PostCall {

        constructor() { 
            console.log("HttpCall:PostCall class loaded!");
        }
        send(data: any, url:any, callbackSuccess: (response: any) => any, callbackError: (response: any) => any,method="POST") {
            console.log("post reached !");
            console.info("data to send:" + JSON.stringify(data));
            var obj = {
                url: url,
                type: method,
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                success: function (serverResp) {
                    /**
                      *Response id parsed !
                      *
                    */
                    callbackSuccess(serverResp);
                },
                error: function (serverResp) {
                    callbackError(serverResp);
                }
            };
            $.ajax(obj);

        }

        /*
        $.ajax({
                url: "@Url.Action("AddUser")",
                type: "POST",
                data: JSON.stringify(jsonObject),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (response) {
                    alert(response.responseText);
            },
                success: function (response) {
                    alert(response);
                }
            });

        */
    }

    export class GenericRespnse{
        public static onSuccess(response: any) {
            console.log(JSON.stringify(response));
        }
        public static onError(response: any) {
            console.log(JSON.stringify(response));
        }
    }

}