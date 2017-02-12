
// run the currently selected effect
function hideAlert(obj) {
    // get effect type from


    //// most effect types need no options passed by default
    var options = {};
    //// some effects have required parameters
    //if (selectedEffect === "scale") {
    //    options = { percent: 0 };
    //} else if (selectedEffect === "transfer") {
    //    options = { to: "#button", className: "ui-effects-transfer" };
    //} else if (selectedEffect === "size") {
    //    options = { to: { width: 200, height: 60 } };
    //}

    // run the effect
    
    $(obj).effect("fold", options, 500);
    $(obj).off("click");
};




$(function () {
    // set effect from select menu value
    $(".alert-dismissible .close").each(function (index) {
        $(this).click(function () {
            hideAlert($(this).parent(".alert"));
        });
    });


    $(".alert-dismissible").each(function (index) {
        $(this).click(function () {
            hideAlert(this);
        });
    });
    $(".alert").each(function (index, value) {
        if (!$(this).hasClass("validation-summary-valid"))
            $(this).hide().fadeIn(1500);
    });



});
