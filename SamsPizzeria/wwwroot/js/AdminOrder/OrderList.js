
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
        startSpinAnimation();
        $.ajax({
            type: "POST",
            url: "/AdminOrder/DeleteOrder",
            data: { orderID: orderId },
            success: function (data) {
                let o = $(`#order-${orderId}`);
                o.remove();
            },
            complete: function () {
                stopSpinAnimation();
            },
            error: function (xhr, status, error) {
                alert("Ett oväntat fel uppstod. Kunde ej ta bort order.");
            }
        });

        console.log("OrderID är " + orderId);
        modal.modal('hide');
    });

});

function onBegin(context) {
    startSpinAnimation();
}


function onComplete(context) {
    stopSpinAnimation();
}

function startSpinAnimation() {
    let statusDiv = $("#status");
    statusDiv.removeClass("animationStop");
    statusDiv.addClass("animationStart");
}

function stopSpinAnimation() {
    setTimeout(function () {
        let statusDiv = $("#status");
        statusDiv.removeClass("animationStart");
        statusDiv.addClass("animationStop");
    }, 400);
}


