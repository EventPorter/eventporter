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

    $(window).scroll(function () {
        //+ $('#site-navbar').outerHeight() if static navbar top
        if ($(window).scrollTop() >= $('#home_gallery').innerHeight()) {
            $('#search-bar').removeClass('relative-searchbar');
            $('#search-bar').addClass('fix-searchbar');
        }
        //+ $('#site-navbar').outerHeight() if static navbar top
        else if ($(window).scrollTop() < $('#home_gallery').innerHeight()) {
            $('#search-bar').removeClass('fix-searchbar');
            $('#search-bar').addClass('relative-searchbar');
        }
    });
});