let map, wedAd, marker;
let geocoder, start;
let markers = [];

function codeAddress(geocoder, wedAd) {

    geocoder.geocode({ 'address': wedAd }, function (results, status) {
        if (status == 'OK') {
            map = new google.maps.Map(document.getElementById("map"), {
                mapId: "deec05c97bc801c2",
                zoom: 12,
                center: results[0].geometry.location,
            });

            marker = new google.maps.Marker({
                position: results[0].geometry.location,
                map,
                title: "Hello World!",
            });

            const contentString =
                '<div id="content">' +
                '<div id="siteNotice">' +
                "</div>" +
                '<h5 class="text-dark" id="firstHeading" class="firstHeading">' +
                wedAd +
                '</h5 > ' +

                "</div>";

            const infowindow = new google.maps.InfoWindow({
                content: contentString,
            });

            marker.addListener("click", () => {
                infowindow.open({
                    anchor: marker,
                    map,
                    shouldFocus: false,
                });
            });

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });

    initAutocomplete();

}

function initMap() {
    //map = new google.maps.Map(document.getElementById("map"), {
    //    center: { lat: 27.964157, lng: -82.452606 },
    //    zoom: 8,
    //    mapId: "deec05c97bc801c2",
    //});

    // to established the variable 
    geocoder = new google.maps.Geocoder();
    wedAd = document.getElementById("wedAddy").value;

    codeAddress(geocoder, wedAd);

}


function initAutocomplete() {
    start = document.getElementById("start");

    // Create the autocomplete object, restricting the search predictions to
    // addresses in the US and Canada.
    autocomplete = new google.maps.places.Autocomplete(start, {
        componentRestrictions: { country: ["us", "ca"] },
        fields: ["address_components", "geometry"],
        types: ["address"],
    });

}

const btn = document.getElementById("useBtn");

btn.addEventListener('click', (e) => {
    e.preventDefault();
    setMapOnAll(null);
    const panel = document.getElementById("panel");
    panel.style.visibility = 'visible';

    const directionsRenderer = new google.maps.DirectionsRenderer({
        draggable: true,
        map,
        panel: panel,
    });
    const directionsService = new google.maps.DirectionsService();


    const userAddress = document.getElementById('start').value;
    const destination = document.getElementById("wedAddy").value;

    directionsService.route({
        origin: userAddress,
        destination: destination,
        travelMode: google.maps.TravelMode.DRIVING,
    })
        .then((response) => {
            console.log(response)
            directionsRenderer.setDirections(response);
        })
        .catch((e) => console.log("Directions request failed due to " + status));
})

function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}


//// This is the section to add the validations of pastdate to the front

$.validator.addMethod('pastdate', function (value, element, params) {
    return value === value.PastDate();
});

$.validator.unobtrusive.adapters.addBool("pastdate");