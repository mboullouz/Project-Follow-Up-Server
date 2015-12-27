module Home {
    export class Index {
        constructor() {
            console.log('Hom->Index loaded ');
            $('#updateTasks').click(this.handleClickCallServer);
            
        }
        handleClickCallServer = (event) => {
            $('#notif').text('Operation done! ');
            $('#notif').show(); 
            $('#notif').click(function (evt) { $('#notif').hide(); });
        }
    }
}

var app = new Home.Index();