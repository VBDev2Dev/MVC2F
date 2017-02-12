

function ShowWait(message) {
    if (message !== undefined && message.length > 0)
        $("#pleaseWaitDialogHeader").replaceWith(message);
    $("#pleaseWaitDialog").modal();

}

function HideWait() {
    setTimeout(function(){
        $("#pleaseWaitDialog").modal('hide');
    }, 1000);
   
}