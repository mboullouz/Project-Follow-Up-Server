/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../Shared/Models/Entity.ts" />
/// <reference path="../Shared/HttpCall.ts" />
class TaskApp {
    taskBasic: Entity.TaskBasic = new Entity.TaskBasic();
    httpPost: HttpCall.PostCall = new HttpCall.PostCall();
    changeStateUrl: string;
    constructor() {
        
        this.changeStateUrl = $("#conf").data("url");
        $(".changeState").click(this.handleChangeState);
        console.log("TaskApp is loaded \t urlStateChange:" + this.changeStateUrl);
    }
    public handleChangeState = (event) => {
        var element = event.target; 
        var idTask = $(element).attr('id');
        this.changeTaskState(idTask, true);
    }
    public changeTaskState(idTask, state) {
        this.taskBasic.setId(idTask);
        this.taskBasic.setDone(state);
        this.httpPost.send(this.taskBasic, this.changeStateUrl, this.onSuccess, HttpCall.GenericRespnse.onError);
    }
    onSuccess(response) {
        console.log("Response >>  \n"+JSON.stringify(response));
        var parentTr = $("#" + response.IdEntity).closest("tr");
        $(parentTr).addClass("done");
    }

}

var taskApp = new TaskApp();