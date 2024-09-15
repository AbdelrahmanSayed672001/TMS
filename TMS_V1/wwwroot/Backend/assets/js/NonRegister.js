$(document).ready(function () {
    // Function to handle the search and update the table
    function searchAndUpdateTable(titleSearch, page) {
        $.ajax({
            url: '/Backend/Nationals/NonRegisterSearch', // Replace 'YourController' with your actual controller name
            type: 'GET',
            data: { titleSearch: titleSearch, page: page },
            success: function (result) {
                // Update the table with the new data
                $('#NonRegisterTable').html(result);
            },
            error: function (error) {
                console.error('Error occurred while fetching data:', error);
            }
        });
    }

    $('#searchButton').on('click', function () {
        var titleSearch = $('#titleSearch').val();
        var page = 1; // You may get the page number from some element if needed
        searchAndUpdateTable(titleSearch, page);
    });
    $("#titleSearch").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            $("#searchButton").click();
        }
    });
});
