// Global variables 
let map; 
let markers = [];
let infoWindow;
let currentSelected;

// Initialize the map at users location
function initMap(position) {

    let lat = position.coords.latitude;
    let lng = position.coords.longitude;

    // centre coordinates for the map
    const myMapCenter = {
        lat: lat,
        lng: lng
    }

    infoWindow = new google.maps.InfoWindow();

    // create map and target HTML element to put map in
    map = new google.maps.Map(document.getElementById('map'), {
        center: myMapCenter,
        zoom: 14
    });

    // create marker and set its position
    const marker = new google.maps.Marker({
        map: map,
        position: myMapCenter,
        title: 'my location'
    });

    // info window for current location
    google.maps.event.addListener(marker, 'click', function () {
        infoWindow.setContent(
            '<h5>Your current location</h5>'
        );
        infoWindow.open(map, marker);
    });
}

// Function to get user location 
function getLocation(){
    {
        if (navigator.geolocation)

        {
            var options = {
                enableHighAccuracy: true,
                timeout: 5000,
                maximumAge: 0
            };
            navigator.geolocation.getCurrentPosition( initMap, error, options);
        }
        else
        { x.innerHTML= "Geolocation is not supported by this browser."; }
    }
}

// Function to clear markers from the map
function clearLocations() {
    infoWindow.close();
    for(let i = 0; i < markers.length; i++){
        markers[i].setMap(null);
    }
    markers.length = 0;
}

// Re-initialize the map with markers at all walmart locations
function getWalmartLocations() {

    clearLocations();

    const walmartStores = [
        {
            position: new google.maps.LatLng(43.775779724121094, -79.26200866699219),
            type: 'store',
            title: 'Walmart Scarborough Town Centre',
            address: '300 Borough Dr, Scarborough, ON M1P 4P5'
        },
        {
            position: new google.maps.LatLng(43.742699, -79.224022),
            type: 'store',
            title: 'Walmart Pharmacy',
            address: '3132 Eglinton Ave E, Scarborough, ON M1J 2H1'
        },
        {
            position: new google.maps.LatLng(43.726639, -79.294067),
            type: 'store',
            title: 'Walmart Scarborough West',
            address: '1900 Eglinton Ave E, Scarborough, ON M1L 2L9'
        },
        {
            position: new google.maps.LatLng(43.794640, -79.235020),
            type: 'store',
            title: 'Walmart Agincourt',
            address: '3850 Sheppard Ave E, Scarborough, ON M1T 3L3'
        },
        {
            position: new google.maps.LatLng(43.667987, -79.484753),
            type: 'store',
            title: 'Walmart Junction',
            address: '2525 St Clair Ave W, Toronto, ON M6N 4Z5'
        },
        {
            position: new google.maps.LatLng(43.757708, -79.488687),
            type: 'store',
            title: 'Walmart Downsview',
            address: '3757 Keele St, North York, ON M3J 1N4'
        },
        {
            position: new google.maps.LatLng(43.721766, -79.511720),
            type: 'store',
            title: 'Walmart Sheridan Mall',
            address: '2202 Jane St, North York, ON M3M 1A4'
        },
        {
            position: new google.maps.LatLng(43.8106051, -79.4528302),
            type: 'store',
            title: 'Walmart Thornhill',
            address: '700 Centre St, Thornhill, ON L4J 0A7'
        }
    ]

    // creates a marker for each location
    for (let i = 0; i < walmartStores.length; i++){
        let html = "<b>" + walmartStores[i].title + "</b> <br/>" + walmartStores[i].address;

        let marker = new google.maps.Marker({
            position: walmartStores[i].position,
            map: map
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', function() {
            currentSelected = walmartStores[i].address;

            map.panTo(this.getPosition());

            infoWindow.setContent(html + '<br/>' +
                '<button type="button" class="btn btn-primary location-button" onclick="setStoreLocation()">Set Location</button>');
            infoWindow.open(map, marker);
          });
    }

    map.setZoom(11);
}

// Re-initialize the map with markers at all Loblaws locations
function getLoblawsLocations(){

    clearLocations();
    
    const loblawsStores = [
        {
            position: new google.maps.LatLng(43.735304, -79.404278),
            type: 'store',
            title: 'Loblaws Yonge and Yonge',
            address: '3501 Yonge St, North York, ON M4N 2N5'
        },
        {
            position: new google.maps.LatLng(43.707165, -79.394601),
            type: 'store',
            title: 'Loblaws Yonge and Eglinton',
            address: '101 Eglinton Ave E #1, Toronto, ON M4P 1H4'
        },
        {
            position: new google.maps.LatLng(43.688509, -79.288224),
            type: 'store',
            title: 'Loblaws Victoria Park',
            address: '50 Musgrave St, Toronto, ON M4E 3W2'
        },
        {
            position: new google.maps.LatLng(43.661001, -79.328338),
            type: 'store',
            title: 'Loblaws Leslieville',
            address: '17 Leslie St, Toronto, ON M4M 3H9'
        },
        {
            position: new google.maps.LatLng(43.699984, -79.360162),
            type: 'store',
            title: 'Loblaws Leaside',
            address: '11 Redway Rd, East York, ON M4H 1P6'
        },
        {
            position: new google.maps.LatLng(43.685086, -79.356804),
            type: 'store',
            title: 'Loblaws Bayview',
            address: '2877 Bayview Ave, North York, ON M2K 2S3'
        },
        {
            position: new google.maps.LatLng(43.684145, -79.415488),
            type: 'store',
            title: 'Loblaws St Clair',
            address: '396 St Clair Ave W, Toronto, ON M5P 3N3'
        },
        {
            position: new google.maps.LatLng(43.654829, -79.450545),
            type: 'store',
            title: 'Loblaws Dundas',
            address: '2280 Dundas St W, Toronto, ON M6R 1X3'
        }
    ];

    for (let i = 0; i <= loblawsStores.length; i++){
        let html = "<b>" + loblawsStores[i].title + "</b> <br/>" + loblawsStores[i].address;

        let marker = new google.maps.Marker({
            position: loblawsStores[i].position,
            map: map
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', function() {
            currentSelected = loblawsStores[i].address;

            map.panTo(this.getPosition());

            infoWindow.setContent(html + '<br/>' +
                '<button type="button" class="btn btn-primary location-button" onclick="setStoreLocation()">Set Location</button>');
            infoWindow.open(map, marker);
          });
    };

    map.setZoom(11);
}

// Re-initialize the map with markers at all Metro locations
function getMetroLocations() {

    clearLocations();

    const metroStores = [
        {
            position: new google.maps.LatLng(43.7577822, -79.3145775),
            type: 'store',
            title: 'Metro Ellesmere',
            address: '15 Ellesmere Rd, Scarborough, ON M1R 4B7'
        },
        {
            position: new google.maps.LatLng(43.7724563,-79.2805428),
            type: 'store',
            title: 'Metro Kennedy Commons',
            address: '16 William Kitchen Rd, Scarborough, ON M1P 5B7'
        },
        {
            position: new google.maps.LatLng(43.743466,-79.216242),
            type: 'store',
            title: 'Metro Kingston',
            address: '3221 Eglinton Ave E, Scarborough, ON M1J 2H7'
        },
        {
            position: new google.maps.LatLng(43.7243565,-79.3009221),
            type: 'store',
            title: 'Metro Eglinton Square',
            address: '40 Eglinton Square, Scarborough, ON M1L 2K1'
        },
        {
            position: new google.maps.LatLng(43.733898,-79.34375),
            type: 'store',
            title: 'Metro Don Mills',
            address: '1050 Don Mills Rd, North York, ON M3C 1W6'
        },
        {
            position: new google.maps.LatLng(43.7473505,-79.3855761),
            type: 'store',
            title: 'Metro York Mills',
            address: '291 York Mills Rd, North York, ON M2L 1L3'
        },
        {
            position: new google.maps.LatLng(43.719406,-79.430214),
            type: 'store',
            title: 'Metro Bathurst',
            address: '3090 Bathurst St, North York, ON M6A 2A2'
        },
        {
            position: new google.maps.LatLng(43.639949,-79.417868),
            type: 'store',
            title: 'Metro Liberty Village',
            address: '100 Lynn Williams St, Toronto, ON M6K 3N6'
        }
    ];

    for (let i = 0; i <= metroStores.length; i++){
        let html = "<b>" + metroStores[i].title + "</b> <br/>" + metroStores[i].address;

        let marker = new google.maps.Marker({
            position: metroStores[i].position,
            map: map
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', function() {
            currentSelected = metroStores[i].address;

            map.panTo(this.getPosition());

            infoWindow.setContent(html + '<br/>' +
                '<button type="button" class="btn btn-primary location-button" onclick="setStoreLocation()">Set Location</button>');
            infoWindow.open(map, marker);
          });
    };

    map.setZoom(11);
}

// Re-initialize the map with markers at all Costco locations
function getCostcoLocations() {

    clearLocations();

    const costcoStores = [
        {
            position: new google.maps.LatLng(43.759388,-79.297859),
            type: 'store',
            title: 'Costco Warden',
            address: '1411 Warden Ave, Scarborough, ON M1R 2S3'
        },
        {
            position: new google.maps.LatLng(43.731625,-79.282944),
            type: 'store',
            title: 'Costco Eglinton',
            address: '50 Thermos Rd, Scarborough, ON M1L 0E6'
        },
        {
            position: new google.maps.LatLng(43.70568,-79.350027),
            type: 'store',
            title: 'Costco Overlea',
            address: '42 Overlea Blvd, Toronto, ON M4H 1B6'
        },
        {
            position: new google.maps.LatLng(43.732796,-79.451431),
            type: 'store',
            title: 'Costco Downsview',
            address: '100 Billy Bishop Way, North York, ON M3K 2C8'
        },
        {
            position: new google.maps.LatLng(43.732796,-79.451431),
            type: 'store',
            title: 'Costco Vaughn',
            address: '71 Colossus Dr, Woodbridge, ON L4L 9J8'
        },
        {
            position: new google.maps.LatLng(43.622706,-79.507392),
            type: 'store',
            title: 'Costco Etobicoke',
            address: '50 Queen Elizabeth Blvd, Etobicoke, ON M8Z 1M1'
        },
        {
            position: new google.maps.LatLng(43.844673,-79.353907),
            type: 'store',
            title: 'Costco Markham',
            address: '1 Yorktech Dr, Markham, ON L6G 1B6'
        }
    ];

    for (let i = 0; i <= costcoStores.length; i++){
        let html = "<b>" + costcoStores[i].title + "</b> <br/>" + costcoStores[i].address;

        let marker = new google.maps.Marker({
            position: costcoStores[i].position,
            map: map
        });

        markers.push(marker);

        google.maps.event.addListener(marker, 'click', function() {
            currentSelected = costcoStores[i].address;

            map.panTo(this.getPosition());

            infoWindow.setContent(html + '<br/>' +
                '<button type="button" class="btn btn-primary location-button" onclick="setStoreLocation()">Set Location</button>');
            infoWindow.open(map, marker);
          });
    };

    map.setZoom(11);
}

// Set store location 
function setStoreLocation(){

    let store = document.getElementById('defaultStoreLocation');

    store.innerHTML = currentSelected;
}

// Exception handling
function error(e) {
    console.log("error code:" + e.code + 'message: ' + e.message );
}