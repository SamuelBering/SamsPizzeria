$(".submitBtn").click(() => {
    startSpinAnimation();
});

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
