$(document).ready(function () {
    fillData();
    createMap();
});
function fillData() {
    coordinates.push(new google.maps.LatLng(42.178416099796,23.588504791259766));
    coordinates.push(new google.maps.LatLng(42.15398687759265, 23.593482971191406));
    coordinates.push(new google.maps.LatLng(42.15067802855498, 23.592967987060547));
    coordinates.push(new google.maps.LatLng(42.140623156752284, 23.59485626220703));
    coordinates.push(new google.maps.LatLng(42.13387658821469, 23.590564727783203));
    coordinates.push(new google.maps.LatLng(42.13400388861329, 23.583354949951172));
}
var event;
var coordinates = [];
var coordinatesManual = [];
var map;
var flightPath;
var flightPathManual;
function createMap() {
    var myLatlng = new google.maps.LatLng(42.15627751790355, 23.59760284423828);

    var mapOptions = {
        center: myLatlng,
        zoom:13
    };
    map = new google.maps.Map(document.getElementById("map"),
        mapOptions);
    for (i = 0; i < coordinates.length; i++) {
        var marker = new google.maps.Marker({
            position: coordinates[i],
            map: map,
            title: "Hello World!"
        });
    }

    flightPath = new google.maps.Polyline({
        path: coordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    flightPath.setMap(map);

    google.maps.event.addListener(map, 'click', function (ev) {
        event = ev;
        console.info(event.latLng);
        coordinatesManual.push(event.latLng);
        if (flightPathManual != null) {
            flightPathManual.setMap(null);
        }
        var marker = new google.maps.Marker({
            position: event.latLng,
            map: map,
            title: "Hello World!"
        });
        flightPathManual = new google.maps.Polyline({
            path: coordinatesManual,
            geodesic: true,
            strokeColor: '#FF0000',
            strokeOpacity: 1.0,
            strokeWeight: 2
        });

        flightPathManual.setMap(map);
    });


}

