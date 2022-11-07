


  /*  var paymentCount = parseInt(String(@Model.PaymentOptionList.Count()));

    if (paymentCount == 0) {
        $('#ManagerForm :input, #ManagerForm button').prop('disabled', true);
        $('#messageForm').text('Please create a new Payment option before making recording.');
    }*/

    $('Button').each(function () {
        $(this).on('click', function (arg) {

            if ($(this).prop('id') == 'AddBtn') {
                alert('Test from Add');
            }
            else if ($(this).prop('id') == "") {
                alert('Test from Add');

            }
        });
    });

    $('#durationList').on('change', function (event) {
        $('#selectorDuration').submit();

    });





