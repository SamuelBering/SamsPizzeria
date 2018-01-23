$(".addToCartButton").click(function () {
    document.getElementById("productsContatainer").className = "col-lg-7";
    document.getElementById("cartContainer").className = "col-lg-5";

});

function updateCartViewComponent() {
    $("#cart-summary-container").load("/Cart/UpdateCartSummary");
}

