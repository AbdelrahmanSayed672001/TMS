function confirmDelete(ID, Name, ControllerName) {
    
    Swal.fire({
        title: "Are you sure to delete " +Name + "?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: `Yes`,
        cancelButtonText: `Cancel`,
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                //url: `/Backend/` + ControllerName + `/Delete/` + ID,
                url: `/` + ControllerName + `/Delete/` + ID,
                method: 'GET',
                data: { id: ID },
                success: function (response) {
                    swal.fire({
                        title: `Success`,
                        text: Name + `is deleted successfully`,
                        icon: "success"
                    });
                    setTimeout(function () {
                        location.reload();
                    }, 2000);


                },
                error: function () {
                    swal.fire({
                        title: `Error`,
                        text: "Something went wrong, please try again",
                        icon: "error"
                    });
                }
            });
        }
    });
    
}