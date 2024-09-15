$(function editItem(id) {
    $.post('/Backend/Trainees/EditTrainee/' + id, function (data) {
        $('#editModal_' + id + ' .modal-body').html(data);
        $('#editModal_' + id).modal('show');
    });
});

$(function () {
    setTimeout(function () {
        $('#DivSuccess').fadeOut();
    }, 3000);
})



/*$(document).ready(function () {
    // Function to handle the search and update the table for non trainees certificates
    function searchAndUpdateTableForNonCertified(titleSearch, page, sortOrder, governorateId, gender, educationalLevel,
        studyStatus, trainingTime, disabilityType, traineeStatus) {
        
        
        $.ajax({
            url: '/Backend/Trainees/SearchForNonCertified', // Replace 'YourController' with your actual controller name
                type: 'GET',
                data: {
                    titleSearch: titleSearch, page: page, sortOrder: sortOrder, governorateId: governorateId, gender: gender, educationalLevel: educationalLevel,
                    studyStatus: studyStatus, trainingTime: trainingTime, disabilityType: disabilityType, traineeStatus: traineeStatus
                },
                success: function (result) {
                    // Update the table with the new data
                    $('#traineesTable').html(result);
                },
                error: function (error) {
                    console.error('Error occurred while fetching data:', error);
                }
            });
        
    }

    
    $('#searchButtonNonCertified').on('click', function (titleSearch, page, sortOrder, governorateId, gender, educationalLevel,
        studyStatus, trainingTime, disabilityType, traineeStatus) {
        titleSearch = $('#titleSearch').val();
        governorateId = $('#governorateId').val();
        gender = $('#gender').val();
        educationalLevel = $('#educationalLevel').val();
        studyStatus = $('#studyStatus').val();
        trainingTime = $('#trainingTime').val();
        disabilityType = $('#disabilityType').val();
        traineeStatus = $('#traineeStatus').val();
        page = page;
        sortOrder = sortOrder;
        searchAndUpdateTableForNonCertified(titleSearch, page, sortOrder, governorateId, gender, educationalLevel, studyStatus, trainingTime, disabilityType, traineeStatus);
    });
    $("#titleSearch").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            $("#searchButton").click();
        }
    });

    
});
*/
$(document).ready(function () {
    // Function to handle the search and update the table for all trainees
    function searchAndUpdateTable(titleSearch, page, sortOrder, governorateId, gender, educationalLevel,
        studyStatus, trainingTime, disabilityType, traineeStatus, url) {
        $.ajax({
                url: '/Backend/Trainees/Search', // Replace 'YourController' with your actual controller name
                type: 'GET',
                data: {
                    titleSearch: titleSearch, page: page, sortOrder: sortOrder, governorateId: governorateId, gender: gender, educationalLevel: educationalLevel,
                    studyStatus: studyStatus, trainingTime: trainingTime, disabilityType: disabilityType, traineeStatus: traineeStatus, url: url
                },
                success: function (result) {
                    // Update the table with the new data
                    $('#traineesTable').html(result);
                },
                error: function (error) {
                    console.error('Error occurred while fetching data:', error);
                }
            });
        
    }
    $('#searchButton').on('click', function (titleSearch, page, sortOrder, governorateId, gender, educationalLevel,
        studyStatus, trainingTime, disabilityType, traineeStatus) {
        titleSearch = $('#titleSearch').val();
        governorateId = $('#governorateId').val();
        gender = $('#gender').val();
        educationalLevel = $('#educationalLevel').val();
        studyStatus = $('#studyStatus').val();
        trainingTime = $('#trainingTime').val();
        disabilityType = $('#disabilityType').val();
        traineeStatus = $('#traineeStatus').val();
        page = page;
        sortOrder = sortOrder;
        var flag = $(this).data('flag');
        var url = window.location.href;
        //console.log(url);
        searchAndUpdateTable(titleSearch, page, sortOrder, governorateId, gender, educationalLevel, studyStatus, trainingTime, disabilityType, traineeStatus, url);
    });
    $("#titleSearch").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            $("#searchButton").click();
        }
    });
});


// Script for handling Lockout for all trainees
$('#traineesTable').on('click', '.setLockoutButton', function () {
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
        confirmButtonText: " Yes lockout it !!",
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
                        location.reload();
                    }, 3000);
                },
                error: function () {
                    // Handle errors appropriately
                    swal.fire({
                        title: "Error",
                        text: "Something went wrong",
                        icon: "error"
                    });
                }
            });
        }
    });
});

// Script for setting unlockout for all trainees
$('#traineesTable').on('click', '.setUnLockoutButton', function () {
    var btn = $(this);
    const swal = Swal.mixin({
        customClass: {
            cancelButton: "btn btn-danger",
            confirmButton: "btn btn-success mr-2"
        },
        buttonsStyling: false
    });

    swal.fire({
        title: `${btn.data('name')} Do you want to unlockout `,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: " Yes unlockout it !!",
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
                        location.reload();
                    }, 3000);
                },
                error: function () {
                    // Handle errors appropriately
                    swal.fire({
                        title: "Error",
                        text: "Something went wrong",
                        icon: "error"
                    });
                }
            });
        }
    });
});


// Script for handling Lockout for non certified
$('#nonTraineesCertificates').on('click', '.setLockoutButton', function () {
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
        confirmButtonText: " Yes lockout it !!",
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
                        location.reload();
                    }, 3000);
                },
                error: function () {
                    // Handle errors appropriately
                    swal.fire({
                        title: "Error",
                        text: "Something went wrong",
                        icon: "error"
                    });
                }
            });
        }
    });
});

// Script for setting unlockout for non certified
$('#nonTraineesCertificates').on('click', '.setUnLockoutButton', function () {
    var btn = $(this);
    const swal = Swal.mixin({
        customClass: {
            cancelButton: "btn btn-danger",
            confirmButton: "btn btn-success mr-2"
        },
        buttonsStyling: false
    });

    swal.fire({
        title: `${btn.data('name')} Do you want to unlockout `,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: " Yes unlockout it !!",
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
                        location.reload();
                    }, 3000);
                },
                error: function () {
                    // Handle errors appropriately
                    swal.fire({
                        title: "Error",
                        text: "Something went wrong",
                        icon: "error"
                    });
                }
            });
        }
    });
});



// Script for setting lockout
//$('#traineesTable').on('click', '.setLockoutButton', function () {
//    var btn = $(this);
//    const swal = Swal.mixin({
//        customClass: {
//            cancelButton: "btn btn-danger",
//            confirmButton: "btn btn-success mr-2"
//        },
//        buttonsStyling: false
//    });

//    swal.fire({
//        title: `Do you want to lockout ${btn.data('name')}`,
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonText: " Yes Lockout it !!",
//        cancelButtonText: "No, cancel !!",
//        reverseButtons: true
//    }).then((result) => {
//        if (result.isConfirmed) {
//            // Make an AJAX request to the Create action
//            $.ajax({
//                url: `/Backend/Trainees/SetLockoutTrainee`, // Adjust the URL to match your route
//                method: 'POST', // Use POST for creating resources
//                data: { UserId: btn.data('userid') }, // Pass the necessary data
//                success: function (response) {
//                    // Assuming the Create action returns a success message
//                    swal.fire({
//                        title: ` ${btn.data('name')} is lockedout successfully`,
//                        icon: "success"
//                    });
//                    setTimeout(function () {
//                        btn.closest('tr').load(location.href + ' #userTable');
//                    }, 3000);
//                },
//                error: function () {
//                    // Handle errors appropriately
//                    swal.fire({
//                        title: "Error",
//                        text: "Something went wrong",
//                        icon: "error"
//                    });
//                }
//            });
//        }
//    });
//});

