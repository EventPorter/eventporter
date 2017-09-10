$(document).ready(function (e) {
    $('.search-panel .dropdown-menu').find('a').click(function (e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('.input-group #search_param').val(param);
    });

    $('#search_button').click(function (e) {
        var search_filter = $('#search_param').val();
        var search_text = $('#search_input').val();
        alert("Filter: " + search_filter + " User input: " + search_text);
    });
});