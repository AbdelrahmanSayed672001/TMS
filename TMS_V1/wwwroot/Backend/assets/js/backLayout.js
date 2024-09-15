var disabledDays = [0, 6];
$('#disabled-days').datepicker({
    language: 'en',
    onRenderCell: function (date, cellType) {
        if (cellType == 'day') {
            var day = date.getDay(),
                isDisabled = disabledDays.indexOf(day) != -1;

            return {
                disabled: isDisabled
            }
        }
    }
})

$('.dropify').dropify({
    messages: {
        'default': 'Drag and drop a file here or click',
        'replace': 'Drag and drop or click to replace',
        'remove': 'Remove',
        'error': 'Ooops, something wrong happended.'
    }
});

function createOptions(number) {
    var options = [], _options;

    for (var i = 0; i < number; i++) {
        var option = '<option value="' + i + '">Option ' + i + '</option>';
        options.push(option);
    }

    _options = options.join('');

    //$('#number')[0].innerHTML = _options;
    //$('#number-multiple')[0].innerHTML = _options;

    $('#number2')[0].innerHTML = _options;
    $('#number2-multiple')[0].innerHTML = _options;
}

var mySelect = $('#first-disabled2');

//createOptions(4000);

$('#special').on('click', function () {
    mySelect.find('option:selected').prop('disabled', true);
    mySelect.selectpicker('refresh');
});

$('#special2').on('click', function () {
    mySelect.find('option:disabled').prop('disabled', false);
    mySelect.selectpicker('refresh');
});

$('#basic2').selectpicker({
    liveSearch: true,
    maxOptions: 1
});