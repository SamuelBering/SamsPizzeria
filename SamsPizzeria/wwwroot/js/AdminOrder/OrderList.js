﻿
$(".orderStatus").change(event => {
    let form = event.currentTarget.parentElement;
    $(form).submit();
});

$('#DetailsOrderModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var orderId = button.data('orderid');
    var detailsOrderDiv = $("#DetailsOrderDiv");
    detailsOrderDiv.empty();
    startSpinAnimation();

    detailsOrderDiv.load(`/AdminOrder/Details/${orderId}`, function () {
        stopSpinAnimation();
    });

});

$('#DeleteOrderModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var orderId = button.data('orderid'); // Extract info from data-* attributes


    var modal = $(this);
    let okButton = $("#okButton");
    okButton.unbind('click');

    okButton.click(() => {
        $(`#deleteOrderForm-${orderId}`).submit();  
        modal.modal('hide');
    });

});

function onError(context) {

}

function onSuccesRemoveOrder(context) {
    let o = $(`#order-${context.orderId}`);
    o.remove();
}

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


