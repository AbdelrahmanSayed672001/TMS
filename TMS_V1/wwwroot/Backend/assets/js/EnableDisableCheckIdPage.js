
function toggleButton() {
    currentvalue = document.getElementById('toggle').value;
    if (currentvalue == "False") {
        document.getElementById("toggle").value = "True";
    } else {
        document.getElementById("toggle").value = "False";
    }
}

$(document).ready(function () {
    $('#toggle').change(function () {
        $.ajax({
            url: '/Backend/Dashboard/TurnOffCheckIdPage',
            type: 'GET',
            success: function (response) {
                toggleButton();
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
});


