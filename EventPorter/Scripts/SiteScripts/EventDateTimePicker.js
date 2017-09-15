$(document).ready(function () {
    $('#startDate').datetimepicker({
        defaultDate: new Date(),
        format: 'DD/MM/YYYY HH:mm'
    });
    $('#endDate').datetimepicker({
        format: 'DD/MM/YYYY HH:mm'
    });
           
    $("#startDate").on("dp.change", function (e) {
        $('#endDate').data("DateTimePicker").minDate(e.date);
    });
    $("#endDate").on("dp.change", function (e) {
        $('#startDate').data("DateTimePicker").maxDate(e.date);
    });
});