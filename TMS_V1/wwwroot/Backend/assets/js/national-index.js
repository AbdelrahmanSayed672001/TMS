$(document).ready(function () {
    $('#nationalTable').on('click', '.nationalDelete', function () {
        var btn = $(this);

        const swal = Swal.mixin({
            customClass: {
                cancelButton: "btn btn-secondary",
                confirmButton: "btn btn-danger mr-2"
            },
            buttonsStyling: false
        });
        swal.fire({
            title: `هل تريد حذف المتدرب ${btn.data('name')} !!!!! `,
            //text: "أذا تم حذف المتدرب ستم مسح جميع بياناته",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "! Yes, delete it",
            cancelButtonText: "! No, cancel",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Backend/Nationals/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        swal.fire({
                            title: "Deleted!",
                            text: "National has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire({
                            title: "Cancelled",
                            text: "Your imaginary file is safe :)",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});
//$('#nationalTable').on('click', '.editNational', function () {
//    console.log('Edit button clicked');
//    var id = $(this).data('id');
//    console.log('ID:', id);
//    // Your edit button click logic here
//});

//$('#nationalTable').on('click', '.editNational', function () {
//    var id = $(this).data('id');

//    $.post('/Backend/Nationals/Edit/' + id, function (data) {
//        $('#editModal_' + id + ' .modal-body').html(data);
//        $('#editModal_' + id).modal('show');
//    });
//});


//$(function editItem(id) {
//    $.post('/Nationals/Edit/' + id, function (data) {
//        $('#editModal_' + id + ' .modal-body').html(data);
//        $('#editModal_' + id).modal('show');
//    });
//});
// show div create in Index View  
$(function () {
    $("#addTrinee").click(function () {
        $("#collapseCreate").addClass('show');
        $("#collapseList").removeClass('show');
    });
});
$(function () {
    setTimeout(function () {
        $('#DivSuccess').fadeOut();
    }, 3000);
});


$(document).ready(function () {
    // Function to handle the search and update the table
    function searchAndUpdateTable(titleSearch, page) {
        $.ajax({
            url: '/Backend/Nationals/Search', // Replace 'YourController' with your actual controller name
            type: 'GET',
            data: { titleSearch: titleSearch, page: page },
            success: function (result) {
                // Update the table with the new data
                $('#nationalTable').html(result);
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


