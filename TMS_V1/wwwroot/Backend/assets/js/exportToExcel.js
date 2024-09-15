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
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(window.location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}
$('#exportBtn').click(function () {
    // Get filter values
    var titleSearch = $('#titleSearch').val();
    var traineeStatus = $('#traineeStatus').val();
    var governorateId = $('#governorateId').val();
    var gender = $('#gender').val();
    var studyStatus = $('#studyStatus').val();
    var educationalLevel = $('#educationalLevel').val();
    var trainingTime = $('#trainingTime').val();
    var disabilityType = $('#disabilityType').val();
    // Get page number from URL
    var page = getUrlParameter('page');
    var flag = $(this).data('flag');
    if (!governorateId) {
        governorateId = getUrlParameter('governorateId');
    }
    if (!titleSearch) {
        titleSearch = getUrlParameter('titleSearch');
    }
    if (!traineeStatus) {
        traineeStatus = getUrlParameter('traineeStatus');
    }
    if (!gender) {
        gender = getUrlParameter('gender');
    }
    if (!studyStatus) {
        studyStatus = getUrlParameter('studyStatus');
    }
    if (!educationalLevel) {
        educationalLevel = getUrlParameter('educationalLevel');
    }
    if (!trainingTime) {
        trainingTime = getUrlParameter('trainingTime');
    }
    if (!disabilityType) {
        disabilityType = getUrlParameter('disabilitiesType');
    }

    console.log(educationalLevel);
    // Append filter values to the export URL
    var exportUrl = '/Backend/Trainees/SaveExcel?titleSearch=' + encodeURIComponent(titleSearch)
        + "&&governorateId=" + encodeURIComponent(governorateId)
        + "&&gender=" + encodeURIComponent(gender)
        + "&&educationalLevel=" + encodeURIComponent(educationalLevel)
        + "&&studyStatus=" + encodeURIComponent(studyStatus)
        + "&&trainingTime=" + encodeURIComponent(trainingTime)
        + "&&disabilityType=" + encodeURIComponent(disabilityType)
        + "&&traineeStatus=" + encodeURIComponent(traineeStatus)
        + "&&flag=" + encodeURIComponent(flag);

    if (page) {
        exportUrl += '&page=' + encodeURIComponent(page);
    }
    // Redirect to the export action with filter parameters
    window.location.href = exportUrl;
});

$('#exportNonCertifiedBtn').click(function () {
    // Get filter values
    var titleSearch = $('#titleSearch').val();
    var traineeStatus = $('#traineeStatus').val();
    var governorateId = $('#governorateId').val();
    var gender = $('#gender').val();
    var studyStatus = $('#studyStatus').val();
    var educationalLevel = $('#educationalLevel').val();
    var trainingTime = $('#trainingTime').val();
    var disabilityType = $('#disabilityType').val();

    // Append filter values to the export URL
    var exportUrl = '/Backend/Trainees/SaveExcel/NonTraineesCertificates?titleSearch=' + encodeURIComponent(titleSearch)
        + "&&traineeStatus=" + encodeURIComponent(traineeStatus)
        + "&&governorateId=" + encodeURIComponent(governorateId)
        + "&&gender=" + encodeURIComponent(gender)
        + "&&studyStatus=" + encodeURIComponent(studyStatus)
        + "&&educationalLevel=" + encodeURIComponent(educationalLevel)
        + "&&trainingTime=" + encodeURIComponent(trainingTime)
        + "&&disabilityType=" + encodeURIComponent(disabilityType)
        ;

    // Redirect to the export action with filter parameters
    window.location.href = exportUrl;
});
