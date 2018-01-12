
$(".orderStatus").change(event => {
    let form = event.currentTarget.parentElement;
    $(form).submit();
});

function onBegin(context) {
    let statusDiv = $("#status");
    statusDiv.removeClass("animationStop");
    statusDiv.addClass("animationStart");   
}



function onComplete(context) {
    setTimeout(function ()
    {
        let statusDiv = $("#status");
        statusDiv.removeClass("animationStart");
        statusDiv.addClass("animationStop");
    }, 400);   
}

