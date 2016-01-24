module Home {
    export class Index {
        
        constructor() {
            console.log('Home->Index loaded ');
            $('#updateTasks').click(this.handleClickCallServer);
            $("#deletedProjectVisibility").click(this.handleDeletedProjectVisibility);
            
            
        }
        handleDeletedProjectVisibility = (event) => {
            if ($('#deletedProjectVisibility').is(':checked')) {
                $('.deleted').hide();
            }
            else {
                $('.deleted').show();
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