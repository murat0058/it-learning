$(function () {

    var overlay = $('.update-claims-overlay');

    var formStartupState = $('#updateClaimsForm').serializeArray().map(function (input) {

        return {
            name: input.name,
            value: input.value === "true" ? true : false
        };

    });

    $('#updateClaimsForm').submit(function (e) {
        overlay.show();
    });

    $('.form-group .checkbox').change(function (e) {

        var $elem = $(this);

        var input = $elem.find('input');

        var currentState = input.prop("checked");
        var prevState = formStartupState.filter(function (inputData) { return inputData.name == input.attr('name') })[0].value;

        if (currentState !== prevState) {
            $elem.addClass('state-changed');
        } else {
            $elem.removeClass('state-changed');
        }

    });
});