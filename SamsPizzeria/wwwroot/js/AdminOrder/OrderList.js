
$(".orderStatus").change(event => {
    let form = event.currentTarget.parentElement;
    $(form).submit();
});

$('#DeleteOrderModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var orderId = button.data('orderid'); // Extract info from data-* attributes


    var modal = $(this);
    let okButton = $("#okButton");
    okButton.unbind('click');

    okButton.click(() => {
        console.log("OrderID är " + orderId);
        modal.modal('hide');
    });
   
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



