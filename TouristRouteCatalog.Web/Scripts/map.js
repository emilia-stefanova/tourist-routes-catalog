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
var places;
function createMap() {
    var myLatlng = new google.maps.LatLng(42.15627751790355, 23.59760284423828);

    var mapOptions = {
        center: myLatlng,
        zoom: 13
    };
    map = new google.maps.Map(document.getElementById("map"),
        mapOptions);

    google.maps.event.addListener(map, 'click', function (event) {
        if (places) {
            places = null;
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers = [];
        }
        addMarker(event.latLng);
        redrawPath();
        serializeCoordinatesToContainer();
    });

    // Create the search box and link it to the UI element.
    var input = /** @type {HTMLInputElement} */(
        document.getElementById('pac-input'));
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    var searchBox = new google.maps.places.SearchBox(
      /** @type {HTMLInputElement} */(input));

    // [START region_getplaces]
    // Listen for the event fired when the user selects an item from the
    // pick list. Retrieve the matching places for that item.
    google.maps.event.addListener(searchBox, 'places_changed', function () {
        places = searchBox.getPlaces();

        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);

            bounds.extend(place.geometry.location);
        }

        map.fitBounds(bounds);
    });
    // [END region_getplaces]

    // Bias the SearchBox results towards places that are within the bounds of the
    // current map's viewport.
    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
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