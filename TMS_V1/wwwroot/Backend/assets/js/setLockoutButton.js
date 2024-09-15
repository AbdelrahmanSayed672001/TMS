
$('#traineesTable').on('click', '.setLockoutButton', function () {
    $('.setLockoutButton').on('click', function () {
        /*var userId = $(this).data('userid');*/
        var btn = $(this);
        const swal = Swal.mixin({
            customClass: {
                cancelButton: "btn btn-danger",
                confirmButton: "btn btn-success mr-2"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: `Do you want to lockout ${btn.data('name')}`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: " Yes Lockout it !!",
            cancelButtonText: "No, cancel !!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                // Make an AJAX request to the Create action
                $.ajax({
                    url: `/Backend/Trainees/SetLockoutTrainee`, // Adjust the URL to match your route
                    method: 'POST', // Use POST for creating resources
                    data: { UserId: btn.data('userid') }, // Pass the necessary data
                    success: function (response) {
                        // Assuming the Create action returns a success message
                        swal.fire({
                            title: ` ${btn.data('name')} is lockedout successfully`,
                            icon: "success"
                        });
                        setTimeout(function () {
                            // $('#userTable').DataTable().ajax.reload();
                            btn.closest('tr').load(location.href + ' #userTable');
                        }, 3000);
                    },
                    error: function () {
                        // Handle errors appropriately
                        swal.fire({
                            title: "wrong",
                            text: "Your imaginary file is safe :)",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});


$(document).ready(function () {
    $('.setUnLockoutButton').on('click', function () {
        /*var userId = $(this).data('userid');*/
        var btn = $(this);
        const swal = Swal.mixin({
            customClass: {
                cancelButton: "btn btn-danger",
                confirmButton: "btn btn-success mr-2"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: `${btn.data('name')} Do you want to unockout `,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: " Yes unLockout it !!",
            cancelButtonText: "No, cancel !!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                // Make an AJAX request to the Create action
                $.ajax({
                    url: `/Backend/Trainees/SetUnLockoutTrainee`, // Adjust the URL to match your route
                    method: 'POST', // Use POST for creating resources
                    data: { UserId: btn.data('userid') }, // Pass the necessary data
                    success: function (response) {
                        // Assuming the Create action returns a success message
                        swal.fire({
                            title: ` ${btn.data('name')} is unlockedout successfully`,
                            icon: "success"
                        });
                        setTimeout(function () {
                            location.reload().document.ready('#indexList');
                        }, 3000);
                    },
                    error: function () {
                        // Handle errors appropriately
                        swal.fire({
                            title: "wrong",
                            text: "Your imaginary file is safe :)",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});