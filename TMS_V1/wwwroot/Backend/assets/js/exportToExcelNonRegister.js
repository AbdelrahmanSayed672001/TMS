//function exportTableToExcel(tableId, filename = 'exported_data') {
//    var table = document.getElementById(tableId);
//    var rows = table.getElementsByTagName('tr');
//    var csvContent = [];

//    // Get the column index to exclude (change '#' to the actual header text of the column)
//    var columnToExclude = Array.from(rows[0].querySelectorAll('th')).findIndex(th => th.innerText === '#');
//    var columnToExcludeOP = Array.from(rows[0].querySelectorAll('th')).findIndex(th => th.innerText === 'Option');

//    // Additional filter criteria (customize this based on your needs)
//    var filterColumnIndex = Array.from(rows[0].querySelectorAll('th')).findIndex(th => th.innerText === 'FilterColumnName');
//    var filterCriteria = 'FilterCriteria';

//    // Loop through rows
//    for (var i = 0; i < rows.length; i++) {
//        var row = [];
//        var cols = rows[i].querySelectorAll('td, th');

//        // Skip the header row
//        if (i === 0) {
//            for (var j = 0; j < cols.length; j++) {
//                if (j !== columnToExclude && j !== columnToExcludeOP) {
//                    row.push(cols[j].innerText);
//                }
//            }
//        } else {
//            // Check filter criteria (customize this based on your needs)
//            var filterColumnValue = cols[filterColumnIndex].innerText;
//            if (filterColumnValue === filterCriteria) {
//                for (var j = 0; j < cols.length; j++) {
//                    if (j !== columnToExclude && j !== columnToExcludeOP) {
//                        row.push(cols[j].innerText);
//                    }
//                }
//            }
//        }

//        // Combine row data into a CSV string
//        csvContent.push(row.join(','));
//    }

//    // Combine CSV content into a blob
//    var csvBlob = new Blob([csvContent.join('\n')], { type: 'text/csv;charset=utf-8;' });

//    // Create a link element to trigger the download
//    var link = document.createElement('a');
//    link.href = window.URL.createObjectURL(csvBlob);
//    link.setAttribute('download', filename + '.csv');
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
//}

$('#exportBtn').click(function () {
    // Get filter values
    var titleSearch = $('#titleSearch').val();
    var page = $(this).data('page');
    
    // Append filter values to the export URL
    var exportUrl = '/Backend/Nationals/SaveExcel?titleSearch=' + encodeURIComponent(titleSearch);

    if (page) {
        exportUrl += '&&page=' + encodeURIComponent(page);
    }
    // Redirect to the export action with filter parameters
    window.location.href = exportUrl;
});
