

// for all trainees
$(document).ready(function () {
    $('#traineesTable').on('click', '.createUserCertificateButton', function () {
        var btn = $(this);

        const swal = Swal.mixin({
            customClass: {
                cancelButton: "btn btn-danger",
                confirmButton: "btn btn-success mr-2"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: `هل تريد إضافة شهادة للمتدرب ${btn.data('name')}`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: " نعم, أضفها !",
            cancelButtonText: " لا, إلغاء !",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: `/Backend/Certificates/Create/${btn.data('id')}`,
                    method: 'POST',
                    data: { id: btn.data('id') },
                    success: function (response) {
                        swal.fire({
                            title: `تم إضافة شهادة للمتدرب ${btn.data('name')} بنجاح`,
                            icon: "success"
                        });
                        setTimeout(function () {
                            location.reload();
                            //parentRow.load(location.href + ' #' + parentRow.attr('id'));
                        }, 1000);


                    },
                    error: function () {
                        swal.fire({
                            title: "فشل",
                            text: "حدث خطأ أثناء إضافة الشهادة",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});

//for non certified
$(document).ready(function () {
    $('#nonTraineesCertificates').on('click', '.createUserCertificateButton', function () {
        var btn = $(this);

        const swal = Swal.mixin({
            customClass: {
                cancelButton: "btn btn-danger",
                confirmButton: "btn btn-success mr-2"
            },
            buttonsStyling: false
        });

        swal.fire({
            title: `هل تريد إضافة شهادة للمتدرب ${btn.data('name')}`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: " نعم, أضفها !",
            cancelButtonText: " لا, إلغاء !",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: `/Backend/Certificates/Create/${btn.data('id')}`,
                    method: 'POST',
                    data: { id: btn.data('id') },
                    success: function (response) {
                        swal.fire({
                            title: `تم إضافة شهادة للمتدرب ${btn.data('name')} بنجاح`,
                            icon: "success"
                        });
                        setTimeout(function () {
                            location.reload();
                            //parentRow.load(location.href + ' #' + parentRow.attr('id'));
                        }, 1000);


                    },
                    error: function () {
                        swal.fire({
                            title: "فشل",
                            text: "حدث خطأ أثناء إضافة الشهادة",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});


//$(function editItem(id) {
//    $.get('/Certificates/Edit/' + id, function (data) {
//        $('#editModal_' + id + ' .modal-body').html(data);
//        $('#editModal_' + id).modal('show');
//    });
//});

$(function () {
    setTimeout(function () {
        $('#DivSuccess').fadeOut();
    }, 3000);
});

