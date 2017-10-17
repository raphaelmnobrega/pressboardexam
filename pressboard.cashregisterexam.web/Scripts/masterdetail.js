document.getElementById("addOrderItem").onclick = function () { myFunction() };

function myFunction() {
    $.ajax({
        async: false,
        url: '/Orders/AddOrderItem'
    }).success(function (partialView) {
        $('#orderItems table tbody:last-child').append(partialView);
    });
    alert("OK!");
}


//$("#addOrderItem").on('click', function () {
//    $.ajax({
//        async: false,
//        url: '/Orders/AddOrderItem'
//    }).success(function (partialView) {
//        $('#orderItems table tbody:last-child').append(partialView);
//    });
//});

//$("a.deleteRow").on("click", function () {
//    $(this).closest('#OrderItem').remove();
//    return false;
//});

