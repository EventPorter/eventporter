$(document).ready(function (e) {
    $('.search-panel .dropdown-menu').find('a').click(function (e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('.input-group #search_param').val(param);
    });

    $('#search_button').click(function (e) {
        search();
    });

    $('#search_input').keypress(function (e) {
        if (e.keyCode == 13) {
            search();
        }
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
    //method below for search events NOT YET COMPLETE??
    //function search() {
    //    var search_filter = $('#search_param').val();
    //    var search_text = $('#searchInput').val();
    //    alert("Filter: " + search_filter + " User input: " + search_text);
    //    $.ajax({
    //        url: '@Url.Action("Event", "EventCardDisplay")',
    //        data: { 'searchParam': search_filter, 'searchInput': search_text },
    //        type: 'post',
    //        cache: false            
    //    });
    //}
    //method below for autocomplete
    //$("#search_input").autocomplete({
    //    source: function (request, response) {
    //        $.ajax({
    //            url: "/Event/getAjaxResult",
    //            type: "POST",
    //            //dataType: "json",
    //            data: { Prefix: request.term },
    //            success: function (data) {
    //                response($.map(data, function (item) {
    //                    return { label: item.Name, value: item.Name };
    //                }))

    //            }
    //        })
    //    },
    //    messages: {
    //        noResults: "", results: ""
    //    }
    //});
});