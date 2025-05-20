
function InitializeValidation() {
    var validator = $("#aspnetForm").bind("invalid-form.validate", function () { }).validate({
        errorElement: "em",
        success: function (label) {
            label.text("").addClass("success");
        }
       ,
        errorPlacement: function (error, element) {
        error.appendTo(element.parent("td").next("td"));
    } /*,
        success: function (label) {
        label.text("ok!").addClass("success");
        }*/

    });
}
