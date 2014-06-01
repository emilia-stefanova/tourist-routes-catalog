$(document).ready(function () {
    createMap();
    fillData();
});
function fillData() {
    var points = $("#pointsContainer").val().split(";");
    for (i = 0; i < points.length; i++) {
        if (points[i] != "") {
            var point = points[i].split(":");
            var myLatlng = new google.maps.LatLng(parseFloat(point[0]), parseFloat(point[1]));
            addMarker(myLatlng);
        }
    }
    redrawPath();
    if (markers.length > 0) {
        centerMap(markers[0].position);
    }
}
var event;
var markers = [];
var map;
var flightPath;
var flightPathManual;
function createMap() {
    var myLatlng = new google.maps.LatLng(42.15627751790355, 23.59760284423828);

    var mapOptions = {
        center: myLatlng,
        zoom: 13
    };
    map = new google.maps.Map(document.getElementById("map"),
        mapOptions);

    google.maps.event.addListener(map, 'click', function (event) {
        addMarker(event.latLng);
        redrawPath();
        serializeCoordinatesToContainer();
    });


}

function serializeCoordinatesToContainer() {
    var serializedCoordinates = "";
    var coordinates = getLatLngFromMarkers();
    for (i = 0; i < coordinates.length; i++) {
        if (serializedCoordinates != "") {
            serializedCoordinates += ";"
        }
        serializedCoordinates += coordinates[i].lat() + ":" + coordinates[i].lng();
    }
    $("#pointsContainer").val(serializedCoordinates);
}

function redrawPath() {
    if (flightPathManual != null) {
        flightPathManual.setMap(null);
    }

    flightPathManual = new google.maps.Polyline({
        path: getLatLngFromMarkers(),
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    flightPathManual.setMap(map);
}

function getLatLngFromMarkers() {
    var coordinates = [];
    for (i = 0; i < markers.length; i++) {
        coordinates.push(markers[i].position);
    }
    return coordinates;
}

function addMarker(myLatlng) {
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map
    });
    markers.push(marker);

    google.maps.event.addListener(marker, 'click', function (event) {
        console.info(marker);
        for (i = 0; i < markers.length; i++) {
            if (marker === markers[i]) {
                markers.splice(i, 1);
            }
        }
        marker.setMap(null);

        redrawPath();
        serializeCoordinatesToContainer();
    });
}

function centerMap(latLng) {
    map.setCenter(latLng);
}