module Home {
    export class Index {
        
        constructor() {
            console.log('Home->Index loaded ');
            $('#updateTasks').click(this.handleClickCallServer);
            $("#deletedProjectVisibility").click(this.handleDeletedProjectVisibility);
            $('.deleted').hide();//initial state
            
        }
       
        handleDeletedProjectVisibility = (event) => {
            if ($('#deletedProjectVisibility').is(':checked')) {
                $('.deleted').show();
            }
            else {
                $('.deleted').hide();
            }
        }
        handleClickCallServer = (event) => {
            $('#notif').text('Operation done! ');
            $('#notif').show(); 
            $('#notif').click(function (evt) { $('#notif').hide(); });
        }
    }
}

var app = new Home.Index();