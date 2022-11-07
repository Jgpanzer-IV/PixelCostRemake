




// Cancel button will clear the content in the input form.
$('#CancelBtn').on('click', function (event) {
    alert('Clear your information');
    $(':input[type=text]').val('');

});