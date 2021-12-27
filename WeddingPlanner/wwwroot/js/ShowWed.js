function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();

    var mapOptions = {
        mapId: "2027f865d98e0d30",
        center: { lat: 27.964, lng: -82.452 },
        zoom: 12,
    }


    const map = new google.maps.Map(document.getElementById("map"), mapOptions);

    directionsRenderer.setMap(map);

    const onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    };

    document.getElementById("from").addEventListener("change", onChangeHandler);
    
}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    directionsService
        .route({
            origin: {
                query: document.getElementById("from").value,
            },
            destination: {
                query: document.getElementById("addy").innerHTML
            },
            travelMode: google.maps.TravelMode.DRIVING,
        })
        .then((response) => {
            directionsRenderer.setDirections(response);
        })
        .catch((e) => console.log("Directions request failed due to " + status));
}


