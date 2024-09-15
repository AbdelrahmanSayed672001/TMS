$(function () {
    $("#addgovernment").click(function () {
        $("#collapseCreate").addClass('show');
        $("#collapseList").removeClass('show');
    });
});
$(function () {
    setTimeout(function () {
        $('#DivSuccess').fadeOut();
    }, 3000);
});