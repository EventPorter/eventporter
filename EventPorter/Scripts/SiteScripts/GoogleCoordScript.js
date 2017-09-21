function updateLocation() {
    var geocoder = new google.maps.Geocoder();
    var loc = document.getElementById('location_input').value;
    //var res = document.getElementById('location_result');
    geocoder.geocode({ 'address': loc }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var latitude = results[0].geometry.location.lat();
            var longitude = results[0].geometry.location.lng();
            document.getElementById('latitude').value = parseFloat(latitude);
            document.getElementById('longitude').value = parseFloat(longitude);
            //alert("lng: " + longitude + " lat: " + latitude);
            //res.innerHTML = "Lat: " + latitude + " Lng: " + longitude;
        }
    });
}