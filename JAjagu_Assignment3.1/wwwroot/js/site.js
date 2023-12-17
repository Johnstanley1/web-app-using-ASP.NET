$(document).ready(function () {
    $('input[type = "datetime"]').datepicker({
        dateFormat: 'mm/dd/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: '-60: +60'
    })
})

function submitForm(invoiceId) {
        document.getElementById('invoiceForm_' + invoiceId).submit();
    }