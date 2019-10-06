var ActionURLUploadImage = "CalculatePermutations";
var totalResult = 0;
function GoToFirstPage() {
    $("#PageIndex").val(1);
    PopulateResult();
}
function GoToPreviousPage() {
    var CurrentPageIndex = parseInt($("#PageIndex").val());
    if (CurrentPageIndex > 1) {
        $("#PageIndex").val(CurrentPageIndex - 1);
        PopulateResult();
    }
}
function GoToNextPage() {
    var CurrentPageIndex = parseInt($("#PageIndex").val());
    var PageSize = parseInt($("#PageSize").val());
    var LastPageIndex = Math.floor(totalResult / PageSize);
    var pagesToAdd = 0;
    if (totalResult % PageSize > 0)
        pagesToAdd = 1;
    if (CurrentPageIndex <= LastPageIndex) {
        $("#PageIndex").val(CurrentPageIndex + 1);
        PopulateResult();
    }
}
function GoToLastPage() {
    var PageSize = parseInt($("#PageSize").val());
    var LastPageIndex = Math.floor(totalResult / PageSize);
    var pagesToAdd = 0;
    if (totalResult % PageSize > 0)
        pagesToAdd = 1;
    $("#PageIndex").val((LastPageIndex + pagesToAdd));
    PopulateResult();
}
function PopulateResult() {
    var phoneNumber = $("#PhoneNumber").val();
    var PageSize = parseInt($("#PageSize").val());
    var PageIndex = parseInt($("#PageIndex").val());
    var PermutationRequest =
        {
            "Number": phoneNumber,
            "PageSize": PageSize,
            "PageIndex": PageIndex
        };
    $.ajax({
        url: ActionURLUploadImage,
        cache: false,
        type: "POST",
        //dataType: 'json',
        //contentType: 'application/json',

        data: PermutationRequest,
        //dataType: 'json',
        //beforeSend: function (xhr) {
        //    $("#Image_Loading").show();
        //},
        success: function (result) {
            $("#DL_Result").html("");
            //$("#Filters").hide('slow');
            //$("#Div_ShowBack").show('slow');
            $("#Div_Result").show('slow');
            var requestResult = $.parseJSON(result);
            console.log(requestResult);
            $("#Span_Number").html(requestResult.Request.Number);
            $("#Span_IsValid").html(requestResult.IsValid);
            $("#Span_ExecutionTime").html(requestResult.Duration);
            $("#Span_PossiblePermutations").html(requestResult.PossiblePermutations.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            $("#Span_ActualPermutations").html(requestResult.ActualPermutations.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " | " + "<b><font color='red'>[" + requestResult.Source +"]</font></b>");
            if (requestResult.IsValid) {
                $("#Div_ResultOK").show('slow');
                $("#Div_ResultError").hide('slow');

                totalResult = requestResult.ActualPermutations
                $.each(requestResult.Permutations, function (i, p) {
                    var counter = parseInt((PageIndex - 1) * PageSize) + parseInt(i + 1);
                    var html = "<dt>" + counter.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "</dt><dd>" + p + "</dd>";
                    $("#DL_Result").append(html);
                })

                if (requestResult.ValidationExplanation != null) {
                    $("#Div_ResultError").show('slow');
                    $("#Span_Error").html(requestResult.ValidationExplanation);
                }
            }
            else {
                totalResult = 0;
                $("#Div_ResultOK").hide('slow');
                $("#Div_ResultError").show('slow');
                $("#Span_Error").html(requestResult.ValidationExplanation);

            }
        },
        error: function (err) {
            console.log("Error: " + err.statusText);
        }

    });
}
$(document).ready(function () {

    $("#Div_ShowBack").hide();
    $("#Div_Result").hide();

    $("#Div_ShowBack").click(function () {
        $("#Div_ShowBack").hide();
        $("#Filters").show('slow');
    })
    $("#Button_Calculate").click(function () {
        PopulateResult();
    });
});