$(document).ready(function () {
    //$('#getLocationBtn').click(function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    const latitude = position.coords.latitude;
                    const longitude = position.coords.longitude;
                    $.ajax({
                        url: '/app/UserLocation', // Your server endpoint
                        type: 'POST',
                        contentType: 'application/json',
                        data: JSON.stringify({ lat: latitude, lon: longitude } ),
                        success: function (response) {
                            $('#locationDisplay').html(response); // Update the results section with new data
                        },
                        error: function (xhr, status, error) {
                            console.error('Error sending location:', error);
                        }
                    });
                },
                function (error) {
                    switch (error.code) {
                        case error.PERMISSION_DENIED:
                            $('#locationDisplay').text("User denied the request for Geolocation.");
                            break;
                        case error.POSITION_UNAVAILABLE:
                            $('#locationDisplay').text("Location information is unavailable.");
                            break;
                        case error.TIMEOUT:
                            $('#locationDisplay').text("The request to get user location timed out.");
                            break;
                        case error.UNKNOWN_ERROR:
                            $('#locationDisplay').text("An unknown error occurred.");
                            break;
                    }
                }
            );
        } else {
            $('#locationDisplay').text("Geolocation is not supported by this browser.");
        }
    //});
});