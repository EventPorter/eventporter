$(document).ready(function () {

    //  gets co-ordinates of location prior to submitting using google api
    $('#Submit1').click(function () {

        if ($.trim($('#location_input').val()).length != 0) {
            updateLocation();

            //  a small paused required to solve an issue requiring user to press submit twice(due to google api)
            //  this causes the submit
            setTimeout(function () {
                if (document.getElementById('latitude').value.length != 0) {
                    $('#form_create').submit();
                }
            }, 1500);
        }
        else {
            alert("location required");
        }
    });
});